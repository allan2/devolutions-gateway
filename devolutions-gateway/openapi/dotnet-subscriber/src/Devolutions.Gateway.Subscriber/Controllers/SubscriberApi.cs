/*
 * devolutions-gateway-subscriber
 *
 * API a service must implement in order to receive Devolutions Gateway notifications
 *
 * The version of the OpenAPI document: 2022.2.2
 * Contact: infos@devolutions.net
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Devolutions.Gateway.Subscriber.Attributes;
using Devolutions.Gateway.Subscriber.Models;

namespace Devolutions.Gateway.Subscriber.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class SubscriberApiController : ControllerBase
    { 
        /// <summary>
        /// Process a message originating from a Devolutions Gateway instance
        /// </summary>
        /// <remarks>Process a message originating from a Devolutions Gateway instance </remarks>
        /// <param name="subscriberMessage">Message</param>
        /// <response code="200">Message received and processed successfully</response>
        /// <response code="400">Bad message</response>
        /// <response code="401">Invalid or missing authorization token</response>
        /// <response code="403">Insufficient permissions</response>
        /// <response code="404">Gateway Subscriber not found</response>
        [HttpPost]
        [Route("/dgw/subscriber")]
        [Authorize]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract IActionResult PostMessage([FromBody]SubscriberMessage subscriberMessage);
    }
}
