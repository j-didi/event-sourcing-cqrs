using EventSourcingCqrs.Core.Ports.Repository;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetEventsHistoryById;

public record GetTodoEventsHistoryByIdQuery(Guid Id) : 
    IRequest<EventHistory>;