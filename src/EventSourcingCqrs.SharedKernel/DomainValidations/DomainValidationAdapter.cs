using FluentValidation.Results;

namespace EventSourcingCqrs.SharedKernel.DomainValidations;

public class DomainValidationAdapter : IDomainValidationPort
{
    private readonly List<DomainValidationFails> _fails = new();

    public void AddNotFound(string description = "Not Found") =>
        _fails.Add(new DomainValidationFails(
            new List<string> { description }, 
            "default", 
            FailType.NotFound
        ));
    
    public void AddFailValidations(ValidationResult validationResult) =>
        _fails.AddRange(validationResult.GetFails());

    public IReadOnlyList<DomainValidationFails> GetFails() =>
        _fails.AsReadOnly();
}