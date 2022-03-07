namespace EventSourcingCqrs.SharedKernel.DomainValidations;

public record DomainValidationFails(
    List<string> Fails,
    string Field,
    FailType Type
);