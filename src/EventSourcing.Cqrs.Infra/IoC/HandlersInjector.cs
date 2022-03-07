using EventSourcingCqrs.Core.Todos.Write.Create;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class HandlersInjector
{
    public static IServiceCollection AddHandlers(
        this IServiceCollection services
    ) => services.AddMediatR(typeof(CreateTodoHandler));
}