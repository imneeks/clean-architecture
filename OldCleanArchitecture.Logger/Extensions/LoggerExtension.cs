using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Logger.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogInformation(this ILogger logger, string category, string message)
        {
            logger.LogInformation("[{Category}] {Message}", category, message);
        }

        public static void LogError(this ILogger logger, string category, string message, Exception ex = null)
        {
            logger.LogError(ex, "[{Category}] {Message}", category, message);
        }

        public static void LogDebug(this ILogger logger, string category, string message)
        {
            logger.LogDebug("[{Category}] {Message}", category, message);
        }
    }
}
