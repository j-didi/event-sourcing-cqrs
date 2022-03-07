using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Write.ChangeDescription;
using EventSourcingCqrs.SharedKernel.DataContracts;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using FluentValidation;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.Done;

public class MarkTodoAsDoneHandler : 
    IRequestHandler<MarkTodoAsDoneCommand, EmptyResult>
{
    private readonly IDomainValidationPort _domainValidator;
    private readonly ITodosPort _todos;

    public MarkTodoAsDoneHandler(
        IDomainValidationPort domainValidator,
        ITodosPort todos
    )
    {
        _domainValidator = domainValidator;
        _todos = todos;
    }
    
    public async Task<EmptyResult> Handle(
        MarkTodoAsDoneCommand command, 
        CancellationToken cancellationToken
    )
    {
        var todo = await _todos.GetById(command.Id);

        if (todo == null)
        {
            _domainValidator.AddNotFound();
            return EmptyResult.Create();
        }
        
        todo.Done();
        await _todos.Update(todo);
        return EmptyResult.Create();
    }
}