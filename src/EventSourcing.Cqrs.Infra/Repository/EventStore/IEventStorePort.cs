using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcing.Cqrs.Infra.Repository.EventStore;

internal interface IEventStorePort
{
    Task Save(Todo todo);
    Task<EventHistory> GetEventHistoryByEntityId(Guid id);
    Task<Todo> GetEntitySnapshot(Guid entityId, Guid eventId);
    Task UndoTo(Guid entityId, Guid eventId);
}