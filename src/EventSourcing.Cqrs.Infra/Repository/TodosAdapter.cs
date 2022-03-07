using EventSourcing.Cqrs.Infra.Repository.EventStore;
using EventSourcing.Cqrs.Infra.Repository.ReadDatabase;
using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Aggregates;
using EventStore.Client;
using MongoDB.Driver;

namespace EventSourcing.Cqrs.Infra.Repository;

public class TodosAdapter: ITodosPort
{
    private readonly IReadDatabasePort _readDatabase;
    private readonly IEventStorePort _eventStore;

    public TodosAdapter(
        IMongoDatabase mongoDatabase,
        EventStoreClient eventStoreClient
    )
    {
        _readDatabase = new MongoAdapter(mongoDatabase);
        _eventStore = new EventStoreAdapter(eventStoreClient);
    }
    
    public async Task Save(Todo todo) =>
        await _readDatabase.Save(todo, () => _eventStore.Save(todo));

    public async Task Update(Todo todo) =>
        await _readDatabase.Update(todo, () => _eventStore.Save(todo));

    public async Task<Todo> GetById(Guid id) =>
        await _readDatabase.GetById(id);

    public async Task<IEnumerable<Todo>> GetAll() =>
        await _readDatabase.GetAll();

    public async Task<EventHistory> GetEventHistoryByEntityId(Guid entityId) =>
        await _eventStore.GetEventHistoryByEntityId(entityId);

    public async Task UndoTo(Guid entityId, Guid eventId)
    {
        var todo = await _readDatabase.GetById(entityId);
        await _readDatabase.Update(todo, () => _eventStore.UndoTo(entityId, eventId));
    }

    public async Task<Todo> GetEntitySnapshot(Guid entityId, Guid eventId) =>
        await _eventStore.GetEntitySnapshot(entityId, eventId);
}