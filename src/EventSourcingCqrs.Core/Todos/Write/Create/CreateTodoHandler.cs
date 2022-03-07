using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Aggregates;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using FluentValidation;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.Create;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CreateTodoResult>
{
    private readonly IValidator<CreateTodoCommand> _validator;
    private readonly IDomainValidationPort _domainValidator;
    private readonly ITodosPort _todos;

    public CreateTodoHandler(
        IValidator<CreateTodoCommand> validator,
        IDomainValidationPort domainValidator,
        ITodosPort todos
    )
    {
        _validator = validator;
        _domainValidator = domainValidator;
        _todos = todos;
    }
    
    public async Task<CreateTodoResult> Handle(
        CreateTodoCommand command, 
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.HasFails())
        {
            _domainValidator.AddFailValidations(validationResult);
            return null;
        }

        var todo = Todo.Create(command.Description);
        await _todos.Save(todo);
        return new CreateTodoResult(todo.Id);
    }
}