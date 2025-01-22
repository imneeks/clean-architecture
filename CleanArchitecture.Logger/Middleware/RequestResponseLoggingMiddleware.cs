using Serilog.Context;
using CleanArchitecture.Logger.Extensions;
using CleanArchitecture.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Serilog;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace CleanArchitecture.Logger.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        // private readonly string[] staticFiles = [".css", ".js", ".jpg", ".jpeg", ".png", ".gif", ".ico", ".woff", ".woff2", ".ttf", ".svg", ".json"];
        private static readonly string[] SensitiveKeys = ["password", "token", "secret", "apikey"];

        public RequestResponseLoggingMiddleware(RequestDelegate requestDelegate, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {


            //if (IsStaticFileRequest(context))
            //{
            //    await _requestDelegate(context); // Skip logging for static files
            //    return;
            //}


            SetInitialLogProperties(context);

            bool hasException = false;

            try
            {
                context.Request.EnableBuffering();
                
                await _requestDelegate(context);
            }
            catch (KeyNotFoundException ex)
            {
                hasException = true;
                _logger.LogInformation("Information", "Not Found - " + ex.Message);
                // Set the response status code
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                // Return a JSON error response
                Result problemDetails = Result.Failure(new Error(StatusCodes.Status404NotFound.ToString(), ex.Message));

                context.Response.ContentType = "application/json";
                var errorJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorJson);

            }
            catch (Exception ex)
            {
                hasException = true;
                _logger.LogError("Error", "An error occurred during processing.", ex);
                // Set the response status code
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Return a JSON error response
                Result problemDetails = Result.Failure(new Error(StatusCodes.Status500InternalServerError.ToString(), "Internal Server Error"));

                context.Response.ContentType = "application/json";
                var errorJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorJson);

            }
            finally
            {


                if (context.Request.Method == "POST" && hasException)
                {
                    var requestBody = await GetSanitizedRequestBodyAsync(context.Request);
                    Log.Information("Request Body: {RequestBody}", requestBody);
                }


                _logger.LogInformation(context.Request.Path, "Process Completed.");
            }
        }

        private void SetInitialLogProperties(HttpContext context)
        {
            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                         ?? Guid.NewGuid().ToString();

            // Add the CorrelationId to the log context
            LogContext.PushProperty("CorrelationId", correlationId);

            // Include it in the response headers for the client
            context.Response.Headers["X-Correlation-ID"] = correlationId;

            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var user = context.User.Identity?.Name ?? "Anonymous";
            LogContext.PushProperty("HttpMethod", context.Request.Method);
            LogContext.PushProperty("UserInfo", user);
            LogContext.PushProperty("HttpPath", context.Request.Path);
            LogContext.PushProperty("CorrelationId", correlationId);
            LogContext.PushProperty("Language", culture.EnglishName);
            _logger.LogInformation("Information", "Process started.");
        }


        private bool IsStaticFileRequest(HttpContext context)
        {
            var path = context.Request.Path.Value;
            return !path.Contains("/api");
            // return staticFiles.Any(ext => path.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }


        /// <summary>
        /// Reads and sanitizes the request body to exclude sensitive data.
        /// </summary>
        private async Task<string> GetSanitizedRequestBodyAsync(HttpRequest request)
        {
            if (request.HasFormContentType)
            {
                // Handle form data
                var form = await request.ReadFormAsync();

                var sanitizedForm = new StringBuilder();
                foreach (var field in form)
                {
                    var value = IsSensitiveKey(field.Key) ? "[REDACTED]" : field.Value.ToString();
                    sanitizedForm.AppendLine($"{field.Key}: {value}");
                }

                return sanitizedForm.ToString();
            }

            // Handle JSON or other text-based requests
            request.Body.Position = 0; // Reset the body stream position
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Reset the position again for downstream middleware

            // Attempt to redact sensitive keys from JSON
            return TryRedactSensitiveJson(body);
        }


        /// <summary>
        /// Checks if a key is considered sensitive.
        /// </summary>
        private static bool IsSensitiveKey(string key)
        {
            return Array.Exists(SensitiveKeys, sensitiveKey =>
                string.Equals(sensitiveKey, key, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Attempts to redact sensitive fields from a JSON string.
        /// </summary>
        private static string TryRedactSensitiveJson(string json)
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(json);
                using var outputStream = new MemoryStream();
                using (var writer = new Utf8JsonWriter(outputStream))
                {
                    RedactJsonElement(jsonDoc.RootElement, writer);
                }

                return Encoding.UTF8.GetString(outputStream.ToArray());
            }
            catch
            {
                // If parsing fails (e.g., not JSON), return the raw body
                return json;
            }
        }

        /// <summary>
        /// Recursively redacts sensitive fields in a JSON element.
        /// </summary>
        private static void RedactJsonElement(JsonElement element, Utf8JsonWriter writer)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                writer.WriteStartObject();
                foreach (var property in element.EnumerateObject())
                {
                    writer.WritePropertyName(property.Name);

                    if (IsSensitiveKey(property.Name))
                    {
                        writer.WriteStringValue("[REDACTED]");
                    }
                    else
                    {
                        RedactJsonElement(property.Value, writer);
                    }
                }
                writer.WriteEndObject();
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                writer.WriteStartArray();
                foreach (var item in element.EnumerateArray())
                {
                    RedactJsonElement(item, writer);
                }
                writer.WriteEndArray();
            }
            else
            {
                element.WriteTo(writer);
            }
        }


    }
}
