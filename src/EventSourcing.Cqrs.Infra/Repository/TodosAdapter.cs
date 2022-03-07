using System.Data;
using EventSourcing.Cqrs.Infra.Repository.EventStore;
using EventSourcing.Cqrs.Infra.Repository.Postgres;
using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Aggregates;
using EventStore.Client;

namespace EventSourcing.Cqrs.Infra.Repository;

public class TodosAdapter: ITodosPort
{
    private readonly IPostgresPort _postgres;
    private readonly IEventStorePort _eventStore;

    public TodosAdapter(
        IDbConnection postgresConnection,
        EventStoreClient eventStoreClient
    )
    {
        _postgres = new PostgresAdapter(postgresConnection);
        _eventStore = new EventStoreAdapter(eventStoreClient);
    }
    
    public async Task Save(Todo todo) =>
        await _postgres.Save(todo, () => _eventStore.Save(todo));

    public async Task Update(Todo todo) =>
        await _postgres.Update(todo, () => _eventStore.Save(todo));

    public async Task<Todo> GetById(Guid id) =>
        await _postgres.GetById(id);

    public async Task<IEnumerable<Todo>> GetAll() =>
        await _postgres.GetAll();

    public async Task<EventHistory> GetEventHistoryByEntityId(Guid entityId) =>
        await _eventStore.GetEventHistoryByEntityId(entityId);

    public async Task UndoTo(Guid entityId, Guid eventId)
    {
        var todo = await _postgres.GetById(entityId);
        await _postgres.Update(todo, () => _eventStore.UndoTo(entityId, eventId));
    }

    public async Task<Todo> GetEntitySnapshot(Guid entityId, Guid eventId) =>
        await _eventStore.GetEntitySnapshot(entityId, eventId);
}