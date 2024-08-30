/*
 * devolutions-gateway
 *
 * Protocol-aware fine-grained relay server
 *
 * The version of the OpenAPI document: 2024.3.2
 * Contact: infos@devolutions.net
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Devolutions.Gateway.Client.Client;
using Devolutions.Gateway.Client.Model;

namespace Devolutions.Gateway.Client.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IJrlApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>JrlInfo</returns>
        JrlInfo GetJrlInfo(int operationIndex = 0);

        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of JrlInfo</returns>
        ApiResponse<JrlInfo> GetJrlInfoWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateJrl(int operationIndex = 0);

        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateJrlWithHttpInfo(int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IJrlApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of JrlInfo</returns>
        System.Threading.Tasks.Task<JrlInfo> GetJrlInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (JrlInfo)</returns>
        System.Threading.Tasks.Task<ApiResponse<JrlInfo>> GetJrlInfoWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateJrlAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateJrlWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IJrlApi : IJrlApiSync, IJrlApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class JrlApi : IJrlApi
    {
        private Devolutions.Gateway.Client.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="JrlApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JrlApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JrlApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JrlApi(string basePath)
        {
            this.Configuration = Devolutions.Gateway.Client.Client.Configuration.MergeConfigurations(
                Devolutions.Gateway.Client.Client.GlobalConfiguration.Instance,
                new Devolutions.Gateway.Client.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Devolutions.Gateway.Client.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Devolutions.Gateway.Client.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Devolutions.Gateway.Client.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JrlApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public JrlApi(Devolutions.Gateway.Client.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Devolutions.Gateway.Client.Client.Configuration.MergeConfigurations(
                Devolutions.Gateway.Client.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Devolutions.Gateway.Client.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Devolutions.Gateway.Client.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Devolutions.Gateway.Client.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JrlApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public JrlApi(Devolutions.Gateway.Client.Client.ISynchronousClient client, Devolutions.Gateway.Client.Client.IAsynchronousClient asyncClient, Devolutions.Gateway.Client.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Devolutions.Gateway.Client.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Devolutions.Gateway.Client.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Devolutions.Gateway.Client.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Devolutions.Gateway.Client.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Devolutions.Gateway.Client.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>JrlInfo</returns>
        public JrlInfo GetJrlInfo(int operationIndex = 0)
        {
            Devolutions.Gateway.Client.Client.ApiResponse<JrlInfo> localVarResponse = GetJrlInfoWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of JrlInfo</returns>
        public Devolutions.Gateway.Client.Client.ApiResponse<JrlInfo> GetJrlInfoWithHttpInfo(int operationIndex = 0)
        {
            Devolutions.Gateway.Client.Client.RequestOptions localVarRequestOptions = new Devolutions.Gateway.Client.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "JrlApi.GetJrlInfo";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (scope_token) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<JrlInfo>("/jet/jrl/info", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetJrlInfo", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of JrlInfo</returns>
        public async System.Threading.Tasks.Task<JrlInfo> GetJrlInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Devolutions.Gateway.Client.Client.ApiResponse<JrlInfo> localVarResponse = await GetJrlInfoWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Retrieves current JRL (Json Revocation List) info 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (JrlInfo)</returns>
        public async System.Threading.Tasks.Task<Devolutions.Gateway.Client.Client.ApiResponse<JrlInfo>> GetJrlInfoWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Devolutions.Gateway.Client.Client.RequestOptions localVarRequestOptions = new Devolutions.Gateway.Client.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "JrlApi.GetJrlInfo";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (scope_token) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<JrlInfo>("/jet/jrl/info", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetJrlInfo", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateJrl(int operationIndex = 0)
        {
            UpdateJrlWithHttpInfo();
        }

        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Devolutions.Gateway.Client.Client.ApiResponse<Object> UpdateJrlWithHttpInfo(int operationIndex = 0)
        {
            Devolutions.Gateway.Client.Client.RequestOptions localVarRequestOptions = new Devolutions.Gateway.Client.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "JrlApi.UpdateJrl";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (jrl_token) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/jet/jrl", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateJrl", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateJrlAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateJrlWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates JRL (Json Revocation List) using a JRL token 
        /// </summary>
        /// <exception cref="Devolutions.Gateway.Client.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Devolutions.Gateway.Client.Client.ApiResponse<Object>> UpdateJrlWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Devolutions.Gateway.Client.Client.RequestOptions localVarRequestOptions = new Devolutions.Gateway.Client.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Devolutions.Gateway.Client.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "JrlApi.UpdateJrl";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (jrl_token) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/jet/jrl", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateJrl", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
