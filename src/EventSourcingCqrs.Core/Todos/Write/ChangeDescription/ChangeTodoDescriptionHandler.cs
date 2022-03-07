using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.SharedKernel.DataContracts;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using FluentValidation;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.ChangeDescription;

public class ChangeTodoDescriptionHandler : 
    IRequestHandler<ChangeTodoDescriptionCommand, EmptyResult>
{
    private readonly IValidator<ChangeTodoDescriptionCommand> _validator;
    private readonly IDomainValidationPort _domainValidator;
    private readonly ITodosPort _todos;

    public ChangeTodoDescriptionHandler(
        IValidator<ChangeTodoDescriptionCommand> validator,
        IDomainValidationPort domainValidator,
        ITodosPort todos
    )
    {
        _validator = validator;
        _domainValidator = domainValidator;
        _todos = todos;
    }
    
    public async Task<EmptyResult> Handle(
        ChangeTodoDescriptionCommand command, 
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.HasFails())
        {
            _domainValidator.AddFailValidations(validationResult);
            return EmptyResult.Create();
        }

        var todo = await _todos.GetById(command.Id);

        if (todo == null)
        {
            _domainValidator.AddNotFound();
            return EmptyResult.Create();
        }
        
        todo.ChangeDescription(command.Description);
        await _todos.Update(todo);
        return EmptyResult.Create();
    }
}