using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static partial class ServiceConfig
{
    public static IServiceCollection AddDbConfig<T>(this IServiceCollection services, string connection) where T : DbContext
    {
        services.AddDbContextPool<T>(x => x.UseSqlServer(GetConnectionString(connection)));
        return services;
    }

    private static string GetConnectionString(string connection)
    {
        if (!Util.AppEnvironment.IsProduction())
            return connection;

        var builder = new SqlConnectionStringBuilder(connection)
        {
            DataSource = "."
        };

        return builder.ConnectionString;
    }
}