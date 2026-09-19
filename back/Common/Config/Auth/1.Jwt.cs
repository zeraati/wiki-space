using FastEndpoints.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static partial class ServiceConfig
{
    public static IServiceCollection AddAuthConfig(this IServiceCollection services, string issuer, string audience)
    {
        services
            .Configure<JwtSigningOptions>(x =>
            {
                x.SigningKey = File.ReadAllText("jwt-public-key.pem");
                x.KeyIsPemEncoded = true;
                x.SigningStyle = TokenSigningStyle.Asymmetric;
            });

        services.AddAuthenticationJwtBearer(x => { },
            y =>
            {
                y.TokenValidationParameters.ValidIssuer = issuer;
                y.TokenValidationParameters.ValidAudience = audience;
            })

             .AddAuthorization();

        return services;
    }
}