using CleanArchitecture.Authentication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CleanArchitecture.Authentication.Configuration;


namespace CleanArchitecture.Authentication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, JwtConfig configuration)
        {
            // Register TokenService
            services.AddScoped<ITokenService, TokenService>();


            // Configure Jwt Bearer authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key)),
                        ValidateIssuer = true,
                        ValidIssuer = configuration.Issuer,
                        ValidateAudience = true,
                        ValidAudience = configuration.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,                       
                    };
                });
            return services;
        }
    }
}