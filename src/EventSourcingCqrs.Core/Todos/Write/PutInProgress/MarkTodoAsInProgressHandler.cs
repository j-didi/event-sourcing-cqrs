using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Write.Done;
using EventSourcingCqrs.SharedKernel.DataContracts;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.PutInProgress;

public class MarkTodoAsInProgressHandler : 
    IRequestHandler<MarkTodoAsInProgressCommand, EmptyResult>
{
    private readonly IDomainValidationPort _domainValidator;
    private readonly ITodosPort _todos;

    public MarkTodoAsInProgressHandler(
        IDomainValidationPort domainValidator,
        ITodosPort todos
    )
    {
        _domainValidator = domainValidator;
        _todos = todos;
    }
    
    public async Task<EmptyResult> Handle(
        MarkTodoAsInProgressCommand command, 
        CancellationToken cancellationToken
    )
    {
        var todo = await _todos.GetById(command.Id);

        if (todo == null)
        {
            _domainValidator.AddNotFound();
            return EmptyResult.Create();
        }
        
        todo.PutInProgress();
        await _todos.Update(todo);
        return EmptyResult.Create();
    }
}