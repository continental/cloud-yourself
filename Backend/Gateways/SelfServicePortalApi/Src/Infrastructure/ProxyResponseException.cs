using System;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Infrastructure
{
    /// <summary>
    /// Exception type for proxy responses.
    /// </summary>
    public class ProxyResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyResponseException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public ProxyResponseException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode { get; }
    }
}
