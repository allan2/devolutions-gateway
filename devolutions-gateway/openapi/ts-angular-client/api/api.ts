export * from './config.service';
import { ConfigService } from './config.service';
export * from './diagnostics.service';
import { DiagnosticsService } from './diagnostics.service';
export * from './health.service';
import { HealthService } from './health.service';
export * from './jrl.service';
import { JrlService } from './jrl.service';
export * from './sessions.service';
import { SessionsService } from './sessions.service';
export const APIS = [ConfigService, DiagnosticsService, HealthService, JrlService, SessionsService];