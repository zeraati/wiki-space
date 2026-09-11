namespace Common.Util;
public static class AppEnvironment
{
    public static bool IsProduction()
        => GetAspNetCoreVariable("ASPNETCORE_ENVIRONMENT") == "Production";

    public static bool IsDevelopment()
        => GetAspNetCoreVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    public static string AppVersion()
        => GetAspNetCoreVariable("APP_VERSION");

    private static string GetAspNetCoreVariable(string key)
        => Environment.GetEnvironmentVariable(key) ?? "";
}