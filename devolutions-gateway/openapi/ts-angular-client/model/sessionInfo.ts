/**
 * devolutions-gateway
 * Protocol-aware fine-grained relay server
 *
 * The version of the OpenAPI document: 2023.2.0
 * Contact: infos@devolutions.net
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { ConnectionMode } from './connectionMode';


/**
 * Information about an ongoing Gateway session
 */
export interface SessionInfo { 
    /**
     * Protocol used during this session
     */
    application_protocol: string;
    /**
     * Unique ID for this session
     */
    association_id: string;
    connection_mode: ConnectionMode;
    /**
     * Destination Host
     */
    destination_host?: string | null;
    /**
     * Filtering Policy
     */
    filtering_policy: boolean;
    /**
     * Recording Policy
     */
    recording_policy: boolean;
    /**
     * Date this session was started
     */
    start_timestamp: string;
    /**
     * Maximum session duration in minutes (0 is used for the infinite duration)
     */
    time_to_live?: number | null;
}
export namespace SessionInfo {
}


