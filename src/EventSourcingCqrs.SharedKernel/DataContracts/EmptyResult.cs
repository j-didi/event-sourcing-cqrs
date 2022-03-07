namespace EventSourcingCqrs.SharedKernel.DataContracts;

public class EmptyResult
{
    private EmptyResult() {}

    public static EmptyResult Create() => new();
}