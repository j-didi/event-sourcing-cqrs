using EventSourcingCqrs.Core.Todos.Write.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class ValidatorsInjector
{
    public static IServiceCollection AddValidators(
        this IServiceCollection services
    ) => services.AddValidatorsFromAssemblyContaining<CreateTodoValidator>();
}