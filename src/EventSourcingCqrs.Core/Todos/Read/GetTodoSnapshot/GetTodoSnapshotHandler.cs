using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetTodoSnapshot;

public class GetTodoSnapshotHandler :
    IRequestHandler<GetTodoSnapshotQuery, GetTodoSnapshotResult>
{
    private readonly ITodosPort _todos;
    private readonly IDomainValidationPort _domainValidator;

    public GetTodoSnapshotHandler(
        ITodosPort todos,
        IDomainValidationPort domainValidator
    )
    {
        _todos = todos;
        _domainValidator = domainValidator;
    }
    
    public async Task<GetTodoSnapshotResult> Handle(
        GetTodoSnapshotQuery query, 
        CancellationToken cancellationToken
    )
    {
        var todo = await _todos.GetEntitySnapshot(query.EntityId, query.EventId);

        if (todo == null)
        {
            _domainValidator.AddNotFound();
            return null;
        }

        return new GetTodoSnapshotResult(todo.Id, todo.Description, todo.Status);
    }
}