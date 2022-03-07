using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetEventsHistoryById;

public class GetTodoEventsHistoryByIdHandler : 
    IRequestHandler<GetTodoEventsHistoryByIdQuery, EventHistory>
{
    private readonly ITodosPort _todos;
    private readonly IDomainValidationPort _domainValidator;

    public GetTodoEventsHistoryByIdHandler(
        ITodosPort todos,
        IDomainValidationPort domainValidator
    )
    {
        _todos = todos;
        _domainValidator = domainValidator;
    }
    
    public async Task<EventHistory> Handle(
        GetTodoEventsHistoryByIdQuery byIdQuery, 
        CancellationToken cancellationToken
    )
    {
        var entity = _todos.GetById(byIdQuery.Id);

        if (entity == null)
        {
            _domainValidator.AddNotFound();
            return null;
        }

        return await _todos.GetEventHistoryByEntityId(byIdQuery.Id);
    }
}