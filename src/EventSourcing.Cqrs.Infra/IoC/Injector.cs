using EventSourcing.Cqrs.Infra.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

public static class Injector
{
    public static void AddAppDependencies(
        this IServiceCollection services,
        AppSettings settings
    ) => services
        .AddSettings(settings)
        .AddHandlers()
        .AddValidators()
        .AddDomainValidator()
        .AddRepositories()
        .AddEventStore(settings)
        .AddMongo(settings);
}