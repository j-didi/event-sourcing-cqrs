using EventSourcing.Cqrs.Infra.Repository;
using EventSourcingCqrs.Core.Ports.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class RepositoryInjector
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services
    ) => services.AddTransient<ITodosPort, TodosAdapter>();
}