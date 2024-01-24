import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input, OnDestroy,
  OnInit,
  Output,
  ViewChild
} from '@angular/core';
import {Observable, of, Subject} from "rxjs";
import {catchError, map, mergeMap, switchMap, takeUntil, tap} from 'rxjs/operators';
import {UserInteraction, SessionEvent, UserIronRdpError, NewSessionInfo} from '@devolutions/iron-remote-gui';
import '@devolutions/iron-remote-gui/iron-remote-gui.umd.cjs';
import { WebClientBaseComponent } from "@shared/bases/base-web-client.component";
import {DefaultRDPPort, IronRDPConnectionParameters} from "@shared/services/web-client.service";
import { ApiService } from "@shared/services/api.service";
import {NavigationService} from "@shared/services/navigation.service";

export enum SSPIType {
  Kerberos = 0,
  Negotiate = 1,
  Ntlm = 2,
}
enum ScreenScale {
  Fit = 1,
  Full = 2,
  Real = 3,
}
enum SessionEventType {
  STARTED = 0,
  TERMINATED = 1,
  ERROR = 2
}
enum UserIronRdpErrorKind {
  General = 0,
  WrongPassword = 1,
  LogonFailure = 2,
  AccessDenied = 3,
  RDCleanPath = 4,
  ProxyConnect = 5
}

@Component({
  templateUrl: 'web-client-rdp.component.html',
  styleUrls: ['web-client-rdp.component.scss']
})
export class WebClientRdpComponent extends WebClientBaseComponent implements  OnInit,
                                                                              AfterViewInit,
                                                                              OnDestroy {

  @Input() formData: any | undefined;
  @Output() isInitialized: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() initializationMessage: EventEmitter<Error> = new EventEmitter<Error>();

  @ViewChild('ironGuiElement') ironGuiElement: ElementRef;

  JET_RDP_URL: string = '/jet/rdp';
  loading: boolean = true;
  screenScale = ScreenScale;
  fullScreen: boolean;

  protected removeElement: Subject<any> = new Subject();
  private remoteClient: UserInteraction;

  constructor(private apiService: ApiService) {
    super();
  }

  ngOnInit(): void {
    this.removeWebClientGuiElement();
  }

  ngAfterViewInit(): void {
    this.initiateRemoteClientListener();
  }

  ngOnDestroy(): void {
    this.removeRemoteClientListener();
    this.removeWebClientGuiElement();
    super.ngOnDestroy();
  }

  sendWindowsKey(): void {
    this.remoteClient.metaKey();
  }

  sendCtrlAltDel(): void {
    this.remoteClient.ctrlAltDel();
  }

  sendTerminateSessionCmd(): void {
    // shutdowns the session, not the server
    this.remoteClient.shutdown();
    this.isInitialized.emit(false);
  }

  scaleTo(scale: ScreenScale): void {
    this.fullScreen = scale === ScreenScale.Full;
    this.remoteClient.setScale(scale);
  }

  private initiateRemoteClientListener(): void {
    this.ironGuiElement.nativeElement.addEventListener('ready', (event: Event) => this.readyRemoteClientEventListener(event));
  }

  private removeRemoteClientListener(): void {
    if (this.ironGuiElement && this.readyRemoteClientEventListener) {
      this.ironGuiElement.nativeElement.removeEventListener('ready', this.readyRemoteClientEventListener);
    }
  }

  removeWebClientGuiElement(): void {
    this.removeElement
      .pipe(takeUntil(this.destroyed$))
      .subscribe((): void => {
        this.ironGuiElement.nativeElement.remove();
      });
  }

  private readyRemoteClientEventListener(event: Event): void {
    const customEvent = event as CustomEvent;
    this.remoteClient = customEvent.detail.irgUserInteraction;

    this.initSessionEventHandler();
    this.startConnectionProcess();
  }

  private startConnectionProcess(): void {
    this.getCredentials()
      .pipe(
        takeUntil(this.destroyed$),
        mergeMap(connectionParameters => this.callConnect(connectionParameters))
      ).subscribe();
  }

  private getCredentials(): Observable<IronRDPConnectionParameters> {
    return this.getFormData().pipe(
      switchMap((connectionParameters)=> this.fetchToken(connectionParameters)),
      map((connectionParameters) => connectionParameters),
      catchError(err => {
        throw err;
      })
    );
  }

  private getFormData() : Observable<IronRDPConnectionParameters> {
    const { hostname, username, password, desktopSize, preConnectionBlob } = this.formData;
    const domain: string = '';

    const gatewayHttpAddress: URL = new URL(this.JET_RDP_URL,window.location.href);
    const websocketUrl: string = gatewayHttpAddress.toString().replace("http", "ws");

    //TODO KDC Server URL
    const kdcProxyUrl = undefined;

    const connectionParameters: IronRDPConnectionParameters = {
      username: username,
      password: password,
      host: hostname,
      domain: domain,
      gatewayAddress: websocketUrl,
      screenSize: desktopSize,
      preConnectionBlob: preConnectionBlob,
      kdcProxyUrl: kdcProxyUrl
    };
    return of(connectionParameters);
  }

  private callConnect(connectionParameters: IronRDPConnectionParameters): Observable<void> {

    return this.remoteClient.connect(
      connectionParameters.username,
      connectionParameters.password,
      connectionParameters.host,
      connectionParameters.gatewayAddress,
      connectionParameters.domain,
      connectionParameters.token,
      connectionParameters.screenSize,
      connectionParameters.preConnectionBlob,
      connectionParameters.kdcProxyUrl
    ).pipe(
      takeUntil(this.destroyed$),
      map(connectionData => {
        //connectionData - NewSessionInfo may be useful in future.
      })
    );
  }

  private fetchToken(connectionParameters: IronRDPConnectionParameters): Observable<IronRDPConnectionParameters> {

    //TODO create a proper model
    const data = {
      "content_type": "ASSOCIATION",
      "protocol": "rdp",
      "destination": `tcp://${connectionParameters.host}:${DefaultRDPPort}`,
      "lifetime": 60,
      "session_id": "cbd67c3b-6bb1-492d-a7be-2af0a5e63f6a"
    }

    return this.apiService.generateSessionToken(data).pipe(
      takeUntil(this.destroyed$),
      tap((token: string) => connectionParameters.token = token),
      map(() => connectionParameters),
      catchError(err => {
        console.error('Fetch Token Error:', err);
        throw err;
      })
    );
  }

  private initSessionEventHandler(): void {
    this.remoteClient.sessionListener
      .pipe(takeUntil(this.destroyed$))
      .subscribe((event: SessionEvent): void => {
        switch (event.type) {
          case SessionEventType.STARTED:
            this.handleIronRDPConnectStarted();
            break;
          case SessionEventType.TERMINATED:
            this.handleIronRDPTerminated(event.data);
            break;
          case SessionEventType.ERROR:
            this.handleIronRDPError(event.data);
            break;
        }
    });
  }

  private handleIronRDPConnectStarted(): void {
    this.loading = false;
    this.remoteClient.setVisibility(true);
    this.webClientConnectionSuccess();
  }

  private handleIronRDPTerminated(data: UserIronRdpError | string): void {
    this.emitInitializationMessage(data);
    this.notifyUserAboutConnectionClosed(data);
    this.isInitialized.emit(false);
  }

  private handleIronRDPError(error: UserIronRdpError | string): void {
    this.emitInitializationMessage(error);
    this.notifyUserAboutError(error);
    this.isInitialized.emit(false);
  }

  private emitInitializationMessage(error: UserIronRdpError | string): void {
    const errorMessage: string = typeof error === 'string' ? error : this.getMessage(error.kind());
    this.initializationMessage.emit(new Error(errorMessage));
  }

  private notifyUserAboutError(error: UserIronRdpError | string): void {
    if (typeof error === 'string') {
      this.webClientConnectionFail(error);
    } else {
      this.webClientConnectionFail(this.getMessage(error.kind()), error.backtrace());
    }
  }

  private notifyUserAboutConnectionClosed(error: UserIronRdpError | string): void {
    const errorMessage: string = typeof error === 'string' ? error : this.getMessage(error.kind());
    this.webClientConnectionClosed(errorMessage);
  }

  private getMessage(type: UserIronRdpErrorKind): string {
    switch (type) {
      case UserIronRdpErrorKind.General:
        //TODO var for translation 'UnknownError'
        return 'Unknown Error';
        break;
      case UserIronRdpErrorKind.WrongPassword:
      case UserIronRdpErrorKind.LogonFailure:
        //TODO var for translation 'ConnectionErrorPleaseVerifyYourConnectionSettings'
        return 'Connection error: Please verify your connection settings.';
        break;
      case UserIronRdpErrorKind.AccessDenied:
        //TODO var for translation 'AccessDenied'
        return 'Access denied';
        break;
      default:
        //TODO var for translation 'ConnectionErrorPleaseVerifyYourConnectionSettings'
        return 'Connection error: Please verify your connection settings.';
    }
  }
}