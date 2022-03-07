using FluentValidation.Results;

namespace EventSourcingCqrs.SharedKernel.DomainValidations;

public interface IDomainValidationPort
{
    void AddNotFound(string description = "Not Found");
    void AddFailValidations(ValidationResult validationResult);
    IReadOnlyList<DomainValidationFails> GetFails();
}