using Domain.Interfaces;
using Infra;

namespace WebApi.Configurations;

public static class DbConfig
{
    public static void ConfigureDataBase(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("MySql");
        services.AddSingleton<Infra.DbConfig>(new Infra.DbConfig { ConnectionString = connectionString });
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
    }
}