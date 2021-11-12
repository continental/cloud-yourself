using System;

namespace CloudYourself.Backend.AppServices.Aws.Infrastructure
{
    /// <summary>
    /// Exception to transport an error from a service or business logic to the api controller.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ErrorObjectException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorObjectException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public ErrorObjectException(string errorCode, string message) : base(message)
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

        /// <summary>
        /// Converts to an error object.
        /// </summary>
        /// <returns>The error object.</returns>
        public object ToErrorObject()
        {
            return new { ErrorCode = ErrorCode, ErrorMessage = Message };
        }
    }
}
