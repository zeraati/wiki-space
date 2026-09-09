using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterServiceAttribute : Attribute
{
    public RegisterServiceAttribute() { }
    public RegisterServiceAttribute(ServiceLifetime lifetime) { Lifetime = lifetime; }
    public ServiceLifetime? Lifetime { get; }
}

public static class RegisterServiceExtensions
{
    public static IServiceCollection AddRegisterServiceAttribute(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var types = assemblies.SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.GetCustomAttribute<RegisterServiceAttribute>() != null);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<RegisterServiceAttribute>()!;
            var lifetime = attr.Lifetime ?? ServiceLifetime.Scoped;

            var @interface = type.GetInterface($"I{type.Name}");

            if (@interface != null)
            {
                services.Add(new ServiceDescriptor(@interface, type, ServiceLifetime.Scoped));
            }
            else
            {
                services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Scoped));
            }
        }

        return services;
    }
}
