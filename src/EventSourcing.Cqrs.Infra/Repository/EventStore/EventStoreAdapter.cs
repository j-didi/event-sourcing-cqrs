using System.Text;
using EventSourcing.Cqrs.Infra.Serializer;
using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.Core.Todos.Aggregates;
using EventStore.Client;
using Newtonsoft.Json;

namespace EventSourcing.Cqrs.Infra.Repository.EventStore;

internal class EventStoreAdapter : IEventStorePort
{
    private readonly EventStoreClient _client;

    public EventStoreAdapter(EventStoreClient client)
    {
        _client = client;
    }

    public async Task Save(Todo todo)
    {
        var events = todo.GetEvents()
            .Select(e =>
                new EventData(
                    Uuid.NewUuid(),
                    e.Description,
                    Encoding.UTF8.GetBytes(e.Snapshop)
                ))
            .ToList();

        await _client.AppendToStreamAsync(
            FormatEventName(todo.Id),
            StreamState.Any,
            events
        );
    }

    public async Task<EventHistory> GetEventHistoryByEntityId(Guid id)
    {
        var result = _client.ReadStreamAsync(
            Direction.Forwards,
            FormatEventName(id),
            StreamPosition.Start
        );

        var items = await result
            .OrderByDescending(e => e.Event.Created)
            .Select(e => new EventHistory.EventHistoryItem(
                e.Event.EventId.ToGuid(),
                e.Event.EventType,
                e.Event.Created,
                Encoding.UTF8.GetString(e.Event.Data.ToArray())
            ))
            .ToListAsync();

        return new EventHistory(items);
    }

    public async Task<Todo> GetEntitySnapshot(Guid entityId, Guid eventId)
    {
        var result = _client.ReadStreamAsync(
            Direction.Forwards,
            FormatEventName(entityId),
            StreamPosition.Start
        );

        var resolvedEvent = await result.FirstOrDefaultAsync(e => 
            e.Event.EventId == Uuid.FromGuid(eventId));

        if (resolvedEvent.Event == null)
            return null;

        var json = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
        return json.CustomDeserializeObject<Todo>();
    }

    public async Task UndoTo(Guid entityId, Guid eventId)
    {
        var history = await GetEventHistoryByEntityId(entityId);
        var todo = history.Items
            .Where(e => e.Id == eventId)
            .Select(e => JsonConvert.DeserializeObject<Todo>(e.Snapshot))
            .FirstOrDefault();

        await Save(todo);
    }

    private static string FormatEventName(Guid id) =>
        $"{nameof(Todo).ToLower()}-{id}";
}