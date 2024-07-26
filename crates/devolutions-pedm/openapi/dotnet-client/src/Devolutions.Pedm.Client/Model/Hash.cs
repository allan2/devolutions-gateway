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
    /// Hash
    /// </summary>
    [DataContract(Name = "Hash")]
    public partial class Hash : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hash" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Hash() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Hash" /> class.
        /// </summary>
        /// <param name="sha1">sha1 (required).</param>
        /// <param name="sha256">sha256 (required).</param>
        public Hash(string sha1 = default(string), string sha256 = default(string))
        {
            // to ensure "sha1" is required (not null)
            if (sha1 == null)
            {
                throw new ArgumentNullException("sha1 is a required property for Hash and cannot be null");
            }
            this.Sha1 = sha1;
            // to ensure "sha256" is required (not null)
            if (sha256 == null)
            {
                throw new ArgumentNullException("sha256 is a required property for Hash and cannot be null");
            }
            this.Sha256 = sha256;
        }

        /// <summary>
        /// Gets or Sets Sha1
        /// </summary>
        [DataMember(Name = "Sha1", IsRequired = true, EmitDefaultValue = true)]
        public string Sha1 { get; set; }

        /// <summary>
        /// Gets or Sets Sha256
        /// </summary>
        [DataMember(Name = "Sha256", IsRequired = true, EmitDefaultValue = true)]
        public string Sha256 { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Hash {\n");
            sb.Append("  Sha1: ").Append(Sha1).Append("\n");
            sb.Append("  Sha256: ").Append(Sha256).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
