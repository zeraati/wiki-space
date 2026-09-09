namespace Microsoft.Extensions.Logging;

public static class LoggerExtension
{
    public static void LogInformationCustom(this ILogger logger, string? message)
    {
        logger.LogInformation($"##########{message}");
    }
}
