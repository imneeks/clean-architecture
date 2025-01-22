using CleanArchitecture.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Authentication.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }
            else
            {
                var authorizeAttribute = endpoint.Metadata
                .OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>()
                .FirstOrDefault();

                if (authorizeAttribute == null)
                {
                    await _next(context);
                    return;
                }
            }
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization token is required.");
                return;
            }

            // Assuming TokenService validates the JWT
            var tokenService = context.RequestServices.GetService<ITokenService>();
            var user = tokenService.ValidateToken(token);

            if (user == null)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid or expired token.");
                return;
            }

            context.Items["User"] = user; // Attach user to the request context
            await _next(context);
        }
    }
}
