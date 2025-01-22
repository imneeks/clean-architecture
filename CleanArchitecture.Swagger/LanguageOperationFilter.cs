using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Swagger
{
    internal class LanguageOperationFilter : IOperationFilter
    {

        private readonly IConfiguration _configuration;

        public LanguageOperationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var localizationConfig = _configuration.GetSection("Localization");
            
            if (localizationConfig.Exists())
            {
                var languages = localizationConfig.GetSection("Languages").Get<List<string>>() ?? new List<string>();
                var headerName = localizationConfig["HeaderParams"] ?? "Accept-Language";
                var headerDescription = localizationConfig["HeaderDesc"] ?? "Select response language";


                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = headerName,
                    In = ParameterLocation.Header,
                    Description = headerDescription,
                    Required = true, // Optional header
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = languages.Select(lang => new OpenApiString(lang)).Cast<IOpenApiAny>().ToList()

                    }
                });
            }            
        }
    }
}
