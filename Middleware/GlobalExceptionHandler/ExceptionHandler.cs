using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Middleware.GlobalExceptionHandler
{
    public static class ExceptionHandler
    {
        // <summary>
        /// Handles an exception by logging it and returning a JSON serialized error response.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="logger">The logger instance to log the error.</param>
        /// <param name="errorResponse">The output error response object.</param>
        /// <returns>A JSON string representing the error response.</returns>
        public static string HandleException(Exception ex, Logger logger, out object errorResponse)
        {
            logger.Error(ex, "An error occured in the application");

            errorResponse = new
            {
                Success = false,
                Message = "An error occurred",
                Error = ex.Message
            };

            return JsonSerializer.Serialize(errorResponse);
        }

        /// <summary>
        /// Creates an error response object without serialization.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="logger">The logger instance to log the error.</param>
        /// <returns>An anonymous object representing the error response.</returns>
        public static object CreateErrorResponse(Exception ex, Logger logger)
        {
            logger.Error(ex, "An error occurred in the application");

            return new
            {
                Success = false,
                Message = "An error occurred",
                Error = ex.Message
            };
        }

    }
}