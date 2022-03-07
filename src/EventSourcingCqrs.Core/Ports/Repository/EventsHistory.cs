namespace EventSourcingCqrs.Core.Ports.Repository;

public class EventHistory
{
    public IEnumerable<EventHistoryItem> Items { get; }

    public EventHistory(IEnumerable<EventHistoryItem> items)
    {
        Items = items;
    }

    public record EventHistoryItem(
        Guid Id,
        string Type,
        DateTime OccuredAt,
        string Snapshot
    );
}

