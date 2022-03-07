using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.SharedKernel.DataContracts;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.UndoToVersion;

public class UndoTodoStateToVersionHandler :
    IRequestHandler<UndoTodoStateToVersionCommand, EmptyResult>
{
    private readonly ITodosPort _todos;
    private readonly IDomainValidationPort _domainValidator;

    public UndoTodoStateToVersionHandler(
        ITodosPort todos,
        IDomainValidationPort domainValidator
    )
    {
        _todos = todos;
        _domainValidator = domainValidator;
    }
    
    public async Task<EmptyResult> Handle(
        UndoTodoStateToVersionCommand command, 
        CancellationToken cancellationToken
    )
    {
        var todo = await _todos.GetEntitySnapshot(command.EntityId, command.EventId);

        if (todo == null)
        {
            _domainValidator.AddNotFound();
            return EmptyResult.Create();
        }

        todo.UpdateToSomeHistoryState();
        await _todos.Update(todo);
        return EmptyResult.Create();
    }
}