use crate::config::Conf;
use crate::interceptor::pcap::PcapInspector;
use crate::interceptor::{Dissector, DummyDissector, Interceptor, WaykDissector};
use crate::session::{SessionInfo, SessionMessageSender};
use crate::subscriber::SubscriberSender;
use crate::token::{ApplicationProtocol, Protocol};
use camino::Utf8PathBuf;
use futures::future::Either;
use std::net::SocketAddr;
use std::sync::Arc;
use tokio::io::{AsyncRead, AsyncWrite};
use tokio::sync::Notify;
use typed_builder::TypedBuilder;

#[derive(TypedBuilder)]
pub struct Proxy<A, B> {
    conf: Arc<Conf>,
    session_info: SessionInfo,
    transport_a: A,
    address_a: SocketAddr,
    transport_b: B,
    address_b: SocketAddr,
    sessions: SessionMessageSender,
    subscriber_tx: SubscriberSender,
}

impl<A, B> Proxy<A, B>
where
    A: AsyncWrite + AsyncRead + Unpin,
    B: AsyncWrite + AsyncRead + Unpin,
{
    pub async fn select_dissector_and_forward(self) -> anyhow::Result<()> {
        match self.session_info.application_protocol {
            ApplicationProtocol::Known(Protocol::Wayk) => {
                trace!("WaykDissector will be used to interpret application protocol.");
                self.forward_using_dissector(WaykDissector).await
            }
            // ApplicationProtocol::Known(Protocol::Rdp) => {
            //     debug!("RdpMessageReader will be used to interpret application protocol");
            //     self.build_with_message_reader(
            //         server_transport,
            //         client_transport,
            //         Some(Box::new(RdpMessageReader::new(
            //             HashMap::new(),
            //             Some(DvcManager::with_allowed_channels(vec![
            //                 RDP8_GRAPHICS_PIPELINE_NAME.to_string()
            //             ])),
            //         ))),
            //     )
            //     .await
            // }
            _ => {
                trace!("No dissector available for this protocol. Data received will not be split to get application message.");
                self.forward_using_dissector(DummyDissector).await
            }
        }
    }

    pub async fn forward_using_dissector<D>(self, dissector: D) -> anyhow::Result<()>
    where
        D: Dissector + Send + 'static,
    {
        if let Some(capture_path) = self.conf.capture_path.as_ref() {
            let filename = format!(
                "{}({})-to-{}({})-at-{}.pcap",
                self.address_a.ip(),
                self.address_a.port(),
                self.address_b.ip(),
                self.address_b.port(),
                chrono::Local::now().format("%Y-%m-%d_%H-%M-%S")
            );
            let mut path = Utf8PathBuf::from(capture_path);
            path.push(filename);

            let (client_inspector, server_inspector) =
                PcapInspector::init(self.address_a, self.address_b, path, dissector)?;

            let mut a = Interceptor::new(self.transport_a);
            a.inspectors.push(Box::new(client_inspector));

            let mut b = Interceptor::new(self.transport_b);
            b.inspectors.push(Box::new(server_inspector));

            Proxy {
                transport_a: a,
                transport_b: b,
                conf: self.conf,
                session_info: self.session_info,
                address_a: self.address_a,
                address_b: self.address_b,
                sessions: self.sessions,
                subscriber_tx: self.subscriber_tx,
            }
            .forward()
            .await
        } else {
            self.forward().await
        }
    }

    pub async fn forward(self) -> anyhow::Result<()> {
        let session_id = self.session_info.id();
        let notify_kill = Arc::new(Notify::new());

        crate::session::add_session_in_progress(
            &self.sessions,
            &self.subscriber_tx,
            self.session_info,
            notify_kill.clone(),
        )
        .await?;

        let forward_fut = transport::forward_bidirectional(self.transport_a, self.transport_b);
        tokio::pin!(forward_fut);

        let kill_notified = notify_kill.notified();
        tokio::pin!(kill_notified);

        // NOTE(DGW-86): when recording is required, should we wait for it to start before we forward, or simply spawn
        // a timer to check if the recording is started within a few seconds?

        let res = match futures::future::select(forward_fut, kill_notified).await {
            Either::Left((res, _)) => match res {
                Ok(_) => Ok(()),
                Err(e) if e.kind() == std::io::ErrorKind::UnexpectedEof => Ok(()),
                Err(e) => Err(e),
            },
            Either::Right(_) => Ok(()),
        };

        crate::session::remove_session_in_progress(&self.sessions, &self.subscriber_tx, session_id).await?;

        match res {
            Ok(()) => Ok(()),
            Err(error) if error.kind() == std::io::ErrorKind::ConnectionReset => {
                info!(%error, "forwarding ended");
                Ok(())
            }
            Err(error) => Err(anyhow::Error::new(error).context("forward")),
        }
    }
}
