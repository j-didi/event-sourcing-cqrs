using EventSourcing.Cqrs.Infra.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class SettingsInjector
{
    public static IServiceCollection AddSettings(
        this IServiceCollection services,
        AppSettings settings
    ) => services.AddSingleton(settings);
}