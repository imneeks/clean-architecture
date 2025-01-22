using CleanArchitecture.Logger.Configurations;
using CleanArchitecture.Logger.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Logger.Extensions
{
    public static class LoggerServiceExtension
    {
        public static void AddLoggerService(this IServiceCollection services, IConfiguration configuration) {

            LoggingConfiguration.ConfigureLogging(configuration);
            services.AddSingleton(Log.Logger);
        }

        public static void AddLoggerService(this ILoggingBuilder builder)
        {
            builder.AddSerilog();   
        }
    }
}
