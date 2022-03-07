using System.Data;
using EventSourcing.Cqrs.Infra.Settings;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class PostgresInjector
{
    public static IServiceCollection AddPostgres(
        this IServiceCollection services,
        AppSettings settings
    ) => services.AddScoped<IDbConnection>( _ =>
    {
        var connection = new NpgsqlConnection(settings.DatabaseSettings.PostgresConnectionString);
        connection.Open();
        return connection;
    });
}