using Serilog.Context;
using CleanArchitecture.Logger.Extensions;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Shared;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.APIs.Middleware
{
    public class RequestResponseLogging
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<RequestResponseLogging> _logger;

        private readonly string[] staticFiles = [".css", ".js", ".jpg", ".jpeg", ".png", ".gif", ".ico", ".woff", ".woff2", ".ttf", ".svg", ".json"];

        public RequestResponseLogging(RequestDelegate requestDelegate, ILogger<RequestResponseLogging> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsStaticFileRequest(context))
            {
                await _requestDelegate(context); // Skip logging for static files
                return;
            }

            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                         ?? Guid.NewGuid().ToString();

            // Add the CorrelationId to the log context
            LogContext.PushProperty("CorrelationId", correlationId);

            // Include it in the response headers for the client
            context.Response.Headers["X-Correlation-ID"] = correlationId;

            _logger.LogInformation("Information", "Process started.");
            try
            {
                LogContext.PushProperty("HttpMethod", context.Request.Method);
                LogContext.PushProperty("HttpPath", context.Request.Path);
                LogContext.PushProperty("CorrelationId", correlationId);

                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", "An error occurred during processing.", ex);
                // Set the response status code
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Return a JSON error response
                Result problemDetails = Result.Failure(new Error("Internal Server Error"));

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(problemDetails);

            }
            finally
            {
                _logger.LogInformation(context.Request.Path, "Process Completed.");
            }
        }

        private bool IsStaticFileRequest(HttpContext context)
        {
            var path = context.Request.Path.Value;
            return staticFiles.Any(ext => path.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }


    }
}
