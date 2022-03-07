using EventSourcing.Cqrs.Infra.Settings;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class EventStoreInjector
{
    public static IServiceCollection AddEventStore(
        this IServiceCollection services,
        AppSettings settings
    ) => services.AddEventStoreClient(e =>
    {
        var connection = settings.DatabaseSettings.EventStoreConnectionString;
        e.ConnectivitySettings.Address = new Uri(connection);
        e.ChannelCredentials = ChannelCredentials.Insecure;
    });
}