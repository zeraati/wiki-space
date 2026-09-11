namespace Common;
public static partial class AuthConfig
{
    public readonly static TimeSpan TokenLifetime =
        Util.AppEnvironment.IsProduction() ?TimeSpan.FromMinutes(30)
        : TimeSpan.FromDays(1);

    public readonly static TimeSpan RefreshTokenLifetime =
        Util.AppEnvironment.IsProduction() ? TimeSpan.FromHours(4)
        : TimeSpan.FromDays(1);
}