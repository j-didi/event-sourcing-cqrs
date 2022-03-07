namespace EventSourcingCqrs.SharedKernel.Core;

public abstract class Aggregate
{
    public Guid Id { get; protected set; }
    private readonly List<Event> _events = new();

    protected void AddEvent(string description, Aggregate content) => 
        _events.Add(new Event(description, content));

    public IReadOnlyCollection<Event> GetEvents() => 
        _events.AsReadOnly();

    public void ClearEvents() =>
        _events.Clear();
}