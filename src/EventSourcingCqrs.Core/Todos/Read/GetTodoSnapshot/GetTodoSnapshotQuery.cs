using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetTodoSnapshot;

public record GetTodoSnapshotQuery(Guid EntityId, Guid EventId): 
    IRequest<GetTodoSnapshotResult>;