using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MiddlewareLibrary.Middleware
{
    /// <summary>
    /// Middleware to handle global exceptions in the application.
    /// It catches unhandled exceptions and returns a JSON response.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Constructor to initialize middleware with the next request delegate and logger.
        /// </summary>
        /// <param name="next">The next middleware component in the pipeline.</param>
        /// <param name="logger">Logger for logging error messages.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Middleware invoke method to handle request processing.
        /// If an exception occurs, it will be caught and logged.
        /// </summary>
        /// <param name="context">HTTP context of the request.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Proceed with the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the error details
                _logger.LogError($"Something went wrong: {ex}");

                // Handle the exception and return an appropriate response
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and returns a structured error response in JSON format.
        /// </summary>
        /// <param name="context">HTTP context of the request.</param>
        /// <param name="exception">The caught exception.</param>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = exception.Message // Log exact error for debugging
            };

            var result = JsonSerializer.Serialize(response);

            // Debugging logs
            Console.WriteLine("ExceptionMiddleware triggered!");
            Console.WriteLine($"Exception: {exception.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            return context.Response.WriteAsync(result);
        }

    }

    /// <summary>
    /// Model class for structured error response.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// HTTP status code of the error response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Message describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Converts the error details to a JSON string format.
        /// </summary>
        /// <returns>JSON representation of the error details.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
