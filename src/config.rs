use clap::{crate_name, crate_version, App, Arg};
use url::Url;
use std::env;

#[derive(Debug, Clone)]
pub enum Protocol {
    WAYK,
    RDP,
    UNKNOWN,
}

#[derive(Clone)]
struct ConfigTemp {
    api_key: Option<String>,
    listeners: Vec<String>,
    jet_instance: Option<String>,
    routing_url: Option<String>,
    pcap_filename: Option<String>,
    protocol: Protocol,
    identities_filename: Option<String>,
}

#[derive(Debug, Clone)]
pub struct ListenerConfig {
    pub url: Url,
    pub external_url: Url,
}

#[derive(Debug, Clone)]
pub struct Config {
    api_key: Option<String>,
    listeners: Vec<ListenerConfig>,
    jet_instance: String,
    routing_url: Option<String>,
    pcap_filename: Option<String>,
    protocol: Protocol,
    identities_filename: Option<String>,
}

impl Config {
    pub fn api_key(&self) -> Option<String> {
        self.api_key.clone()
    }

    pub fn listeners(&self) -> &Vec<ListenerConfig> {
        &self.listeners
    }

    pub fn jet_instance(&self) -> String {
        self.jet_instance.clone()
    }

    pub fn routing_url(&self) -> Option<String> {
        self.routing_url.clone()
    }

    pub fn pcap_filename(&self) -> Option<String> {
        self.pcap_filename.clone()
    }

    pub fn protocol(&self) -> &Protocol {
        &self.protocol
    }

    pub fn identities_filename(&self) -> Option<String> {
        self.identities_filename.clone()
    }

    pub fn init() -> Self {
        let cli_app = App::new(crate_name!())
            .author("Devolutions")
            .version(concat!(crate_version!(), "\n"))
            .version_short("v")
            .about("Devolutions-Jet proxy")
            .arg(Arg::with_name("api-key")
                .long("api-key")
                .value_name("JET_API_KEY")
                .help("The api key used by the server to authenticate client queries.")
                .takes_value(true)
                .empty_values(false))
            .arg(Arg::with_name("listeners")
                .short("l")
                .long("listener")
                .value_name("LISTENER_URL")
                .help("An URL on which the server will listen on. Format: <scheme>://<local_iface_ip>:<port>. Supported scheme: tcp, ws, wss")
                .long_help("An URL on which the server will listen on. The external URL returned as candidate can be specified after the listener, separated with a comma. <scheme>://<local_iface_ip>:<port>,<scheme>://<external>:<port>\
                If it is not specified, the external url will be <scheme>://<jet_instance>:<port> where <jet_instance> is the value of the jet-instance parameter.")
                .multiple(true)
                .takes_value(true)
                .number_of_values(1)
            )
            .arg(Arg::with_name("jet-instance")
                .long("jet-instance")
                .value_name("JET_INSTANCE")
                .help("Specific name to reach that instance of JET.")
                .takes_value(true)
            )
            .arg(
                Arg::with_name("routing-url")
                    .short("r")
                    .long("routing-url")
                    .value_name("ROUTING_URL")
                    .help("An address on which the server will route all packets. Format: <scheme>://<ip>:<port>.")
                    .long_help("An address on which the server will route all packets. Format: <scheme>://<ip>:<port>. Scheme supported : tcp and tls. If it is not specified, the JET protocol will be used.")
                    .takes_value(true)
                    .empty_values(false),
            )
            .arg(
                Arg::with_name("pcap-filename")
                    .short("f")
                    .long("pcap-file")
                    .value_name("PCAP_FILENAME")
                    .help("Path of the file where the pcap file will be saved. If not set, no pcap file will be created. WaykNow and RDP protocols can be saved.")
                    .long_help("Path of the file where the pcap file will be saved. If not set, no pcap file will be created. WaykNow and RDP protocols can be saved.")
                    .takes_value(true)
                    .empty_values(false),
            )
            .arg(
                Arg::with_name("protocol")
                    .short("p")
                    .long("protocol")
                    .value_name("PROTOCOL_NAME")
                    .help("Specify the application protocol used. Useful when pcap file is saved and you want to avoid application message in two different tcp packet.")
                    .long_help("Specify the application protocol used. Useful when pcap file is saved and you want to avoid application message in two different tcp packet. If protocol is unknown, we can't be sure that application packet is not split between 2 tcp packets.")
                    .takes_value(true)
                    .possible_values(&["wayk", "rdp"])
                    .empty_values(false)
            )
            .arg(
                Arg::with_name("identities-file")
                    .short("i")
                    .long("identities-file")
                    .value_name("IDENTITIES_FILE")
                    .help("A JSON-file with a list of identities: proxy credentials, target credentials, and target destination")
                    .long_help(r###"
JSON-file with a list of identities: proxy credentials, target credentials, and target destination.
Every credential must consist of 'username' and 'password' fields with a string,
and optional field 'domain', which also a string if it is present (otherwise - null).
The proxy object must be present with a 'proxy' name, the target object with a 'target' name.
The target destination must be a string with a target URL and be named 'destination'.
identities_file example:
'[
    {
        "proxy":{
            "username":"ProxyUser1",
            "password":"ProxyPassword1",
            "domain":null
        },
        "target":{
            "username":"TargetUser1",
            "password":"TargetPassword1",
            "domain":null
        },
        "destination":"192.168.1.2:3389"
    },
    {
        "proxy":{
            "username":"ProxyUser2",
            "password":"ProxyPassword2",
            "domain":"ProxyDomain2"
        },
        "target":{
            "username":"TargetUser1",
            "password":"TargetPassword2",
            "domain":"TargetDomain2"
        },
        "destination":"192.168.1.3:3389"
    }
]'"
                        "###)
                    .takes_value(true)
                    .empty_values(false),
            );

        let matches = cli_app.get_matches();

        let api_key = matches.value_of("api-key").map(std::string::ToString::to_string);

        let listeners = matches.values_of("listeners").expect("At least one listener has to be specified.").into_iter().map(|listener| listener.to_string()).collect();

        let jet_instance = matches.value_of("jet-instance").map(std::string::ToString::to_string);

        let routing_url = matches.value_of("routing-url").map(std::string::ToString::to_string);

        let pcap_filename = matches.value_of("pcap-filename").map(std::string::ToString::to_string);

        let protocol = match matches.value_of("protocol") {
            Some("wayk") => Protocol::WAYK,
            Some("rdp") => Protocol::RDP,
            _ => Protocol::UNKNOWN,
        };

        let identities_filename = matches
            .value_of("identities-file")
            .map(std::string::ToString::to_string);

        let mut config_temp = ConfigTemp {
            api_key,
            listeners,
            jet_instance,
            routing_url,
            pcap_filename,
            protocol,
            identities_filename,
        };

        config_temp.apply_env_variables();

        config_temp.into()
    }
}

impl ConfigTemp {
    fn apply_env_variables(&mut self) {
        if let Ok(val) = env::var("JET_INSTANCE"){
            self.jet_instance = Some(val);
        }

        if let Ok(val) = env::var("JET_API_KEY") {
            self.api_key = Some(val);
        }
    }
}

impl From<ConfigTemp> for Config {
    fn from(temp: ConfigTemp) -> Self {
        let mut listeners = Vec::new();

        let jet_instance = temp.jet_instance.expect("JET_INSTANCE is mandatory. It has to be added on the command line or defined by JET_INSTANCE environment variable.");

        for listener in &temp.listeners {
            let url;
            let external_url;

            if let Some(pos) = listener.find(",") {
                let url_str = &listener[0..pos];
                url = listener[0..pos].parse::<Url>().expect(&format!("Listener {} is an invalid URL.", url_str));

                if listener.len() > pos + 1 {
                    let external_str = listener[pos+1..].to_string();
                    let external_str = external_str.replace("<jet_instance>", &jet_instance);
                    external_url = external_str.parse::<Url>().expect(&format!("External_url {} is an invalid URL.", external_str));
                } else {
                    panic!("External url has to be specified after the comma : {}", listener);
                }
            } else {
                url = listener.parse::<Url>().expect(&format!("Listener {} is an invalid URL.", listener));
                external_url = format!("{}://{}:{}", url.scheme(), jet_instance, url.port_or_known_default().unwrap_or(8080)).parse::<Url>().expect(&format!("External_url can't be built based on listener {}", listener));
            }

            listeners.push(ListenerConfig {
                url,
                external_url
            });
        }

        Config {
            api_key: temp.api_key,
            listeners,
            jet_instance: jet_instance,
            routing_url: temp.routing_url,
            pcap_filename: temp.pcap_filename,
            protocol: temp.protocol,
            identities_filename: temp.identities_filename,

        }
    }
}