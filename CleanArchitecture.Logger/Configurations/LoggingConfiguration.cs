using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Logger.Configurations
{
    public static class LoggingConfiguration
    {
        /// <summary>
        /// Configures and initializes the Serilog logger.
        /// </summary>
        /// <returns>An initialized Serilog Logger.</returns>
        public static void ConfigureLogging(IConfiguration configuration)
        {
            // Build the logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                // .Enrich.FromLogContext() // Enrich logs with CorrelationId
                // .WriteTo.Console()
                // .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
