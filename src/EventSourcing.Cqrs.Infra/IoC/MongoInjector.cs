using EventSourcing.Cqrs.Infra.Settings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class MongoInjector
{
    public static IServiceCollection AddMongo(
        this IServiceCollection services,
        AppSettings settings
    ) => services.AddScoped(_ =>
    {
        var client = new MongoClient(settings.DatabaseSettings.MongoConnectionString);
        return client.GetDatabase(settings.DatabaseSettings.MongoConnectionDatabaseName);
    });
}