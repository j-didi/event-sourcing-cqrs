using Newtonsoft.Json;

namespace EventSourcingCqrs.SharedKernel.Core;

public record Event
{
    public Guid Id { get; }
    public string Description { get; }
    public string Snapshop { get; }

    public Event(string description, Aggregate content)
    {
        Id = content.Id;
        Description = description;
        Snapshop = JsonConvert.SerializeObject(content);
    }
}