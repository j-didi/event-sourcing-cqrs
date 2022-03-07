using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcingCqrs.Core.Ports.Repository;

public interface ITodosPort
{
    Task Save(Todo todo);
    Task Update(Todo todo);
    Task<Todo> GetById(Guid id);
    Task<IEnumerable<Todo>> GetAll();
    Task<EventHistory> GetEventHistoryByEntityId(Guid entityId);
    Task UndoTo(Guid entityId, Guid eventId);
    Task<Todo> GetEntitySnapshot(Guid entityId, Guid eventId);
}