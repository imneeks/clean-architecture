using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CleanArchitecture.Swagger.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerConfig = configuration.GetSection("SwaggerConfig").Get<SwaggerConfig>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture API V1");
                options.RoutePrefix = string.Empty;
                
               
                if(swaggerConfig != null && swaggerConfig.Styles != null && swaggerConfig.Styles.Count > 0)
                {
                    foreach(var style in swaggerConfig.Styles)
                    {
                        options.InjectStylesheet(style);
                    }
                }

                if (swaggerConfig != null && swaggerConfig.Scripts != null && swaggerConfig.Scripts.Count > 0)
                {
                    foreach (var style in swaggerConfig.Scripts)
                    {
                        options.InjectJavascript(style);
                    }
                }
            });

            return app;
        }

        public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection service, IConfiguration configuration)
        {

            var swaggerConfig = configuration.GetSection("SwaggerConfig").Get<SwaggerConfig>();

            if (swaggerConfig != null)
            {
                service.AddSwaggerGen(
                   options =>
                   {
                       options.UseInlineDefinitionsForEnums();
                       options.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
                       {
                           Title = swaggerConfig.ApiName,
                           Version = swaggerConfig.Version,
                           Description = swaggerConfig.Description
                       });

                       options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                       {
                           Type = SecuritySchemeType.ApiKey,
                           Name = "Authorization", // The name of the header
                           In = ParameterLocation.Header, // The location of the header
                           Description = "Please enter JWT Bearer token in the format **'Bearer {token}'**",
                       });

                       options.AddSecurityRequirement(new OpenApiSecurityRequirement
                       {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header
                                },
                                Array.Empty<string>()
                            }
                       });

                       var localizationConfig = configuration.GetSection("Localization");


                       if (localizationConfig.Exists())
                           options.OperationFilter<LanguageOperationFilter>();

                       options.OperationFilter<APICommonResponse>();
                       

                   }
               );


            }

            return service;
        }
    }
}
