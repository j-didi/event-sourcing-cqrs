using EventSourcingCqrs.SharedKernel.DomainValidations;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Cqrs.Infra.IoC;

internal static class DomainValidationInjector
{
    public static IServiceCollection AddDomainValidator(
        this IServiceCollection services
    ) => services.AddScoped<IDomainValidationPort, DomainValidationAdapter>();
}