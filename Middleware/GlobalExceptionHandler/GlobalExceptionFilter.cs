using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.GlobalExceptionHandler
{
    /// <summary>
    /// A global exception filter to handle uncaught exceptions and return a standardized error response.
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly Logger<GlobalExceptionFilter> _logger;
        private Logger logger;

        /// <summary>
        /// Constructor to initialize the logger.
        /// </summary>
        /// <param name="logger">The logger instance to log errors.</param>
        public GlobalExceptionFilter(Logger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method invoked when an exception occurs during request processing.
        /// </summary>
        /// <param name="context">The exception context containing details of the error.</param>
        public override void OnException(ExceptionContext context)
        {
            var errorResponse = ExceptionHandler.CreateErrorResponse(context.Exception, logger);

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}