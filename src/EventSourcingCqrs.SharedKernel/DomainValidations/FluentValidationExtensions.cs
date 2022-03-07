using FluentValidation.Results;

namespace EventSourcingCqrs.SharedKernel.DomainValidations;

public static class FluentValidationExtensions
{
    public static bool HasFails(this ValidationResult context) =>
        !context.IsValid;
    
    public static List<DomainValidationFails> GetFails(this ValidationResult context) =>
        context.Errors
            .GroupBy(e => e.PropertyName)
            .Select(e => new DomainValidationFails(
                e.Select(f => f.ErrorMessage).ToList(), 
                e.Key, 
                FailType.ValidationFail)
            ).ToList();
}