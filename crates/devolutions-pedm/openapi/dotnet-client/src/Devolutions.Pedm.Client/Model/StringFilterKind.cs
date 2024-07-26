/*
 * Devolutions PEDM API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using FileParameter = Devolutions.Pedm.Client.Client.FileParameter;
using OpenAPIDateConverter = Devolutions.Pedm.Client.Client.OpenAPIDateConverter;

namespace Devolutions.Pedm.Client.Model
{
    /// <summary>
    /// Defines StringFilterKind
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StringFilterKind
    {
        /// <summary>
        /// Enum Equals for value: Equals
        /// </summary>
        [EnumMember(Value = "Equals")]
        Equals = 1,

        /// <summary>
        /// Enum Regex for value: Regex
        /// </summary>
        [EnumMember(Value = "Regex")]
        Regex = 2,

        /// <summary>
        /// Enum StartsWith for value: StartsWith
        /// </summary>
        [EnumMember(Value = "StartsWith")]
        StartsWith = 3,

        /// <summary>
        /// Enum EndsWith for value: EndsWith
        /// </summary>
        [EnumMember(Value = "EndsWith")]
        EndsWith = 4,

        /// <summary>
        /// Enum Contains for value: Contains
        /// </summary>
        [EnumMember(Value = "Contains")]
        Contains = 5
    }

}