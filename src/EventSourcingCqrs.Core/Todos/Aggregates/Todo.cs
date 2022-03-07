using EventSourcingCqrs.SharedKernel.Core;

namespace EventSourcingCqrs.Core.Todos.Aggregates;

public class Todo : Aggregate
{
    public string Description { get; private set; }
    public TodoStatus Status { get; private set; }

    public static Todo Create(string description)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Description = description,
            Status = TodoStatus.Created,
        };
        
        todo.AddEvent(TodoEvents.Created, todo);
        return todo;
    }
    
    private Todo() {}

    public void ChangeDescription(string description)
    {
        Description = description;
        AddEvent(TodoEvents.DescriptionChanged, this);
    }

    public void PutInProgress()
    {
        Status = TodoStatus.InProgress;
        AddEvent(TodoEvents.ChangedStatusToInProgress, this);
    }
    
    public void Done()
    {
        Status = TodoStatus.Done;
        AddEvent(TodoEvents.ChangedStatusToDone, this);
    }
    
    public void UpdateToSomeHistoryState() =>
        AddEvent(TodoEvents.UpdateToSomeHistoryState, this);
}