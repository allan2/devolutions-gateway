/* tslint:disable */
/* eslint-disable */
/**
 * devolutions-gateway
 * Protocol-aware fine-grained relay server
 *
 * The version of the OpenAPI document: 2022.2.2
 * Contact: infos@devolutions.net
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface JrlInfo
 */
export interface JrlInfo {
    /**
     * JWT "Issued At" claim of JRL
     * @type {number}
     * @memberof JrlInfo
     */
    iat: number;
    /**
     * Unique ID for current JRL
     * @type {string}
     * @memberof JrlInfo
     */
    jti: string;
}

/**
 * Check if a given object implements the JrlInfo interface.
 */
export function instanceOfJrlInfo(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "iat" in value;
    isInstance = isInstance && "jti" in value;

    return isInstance;
}

export function JrlInfoFromJSON(json: any): JrlInfo {
    return JrlInfoFromJSONTyped(json, false);
}

export function JrlInfoFromJSONTyped(json: any, ignoreDiscriminator: boolean): JrlInfo {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'iat': json['iat'],
        'jti': json['jti'],
    };
}

export function JrlInfoToJSON(value?: JrlInfo | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'iat': value.iat,
        'jti': value.jti,
    };
}
