using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcingCqrs.Core.Todos.Read.GetTodoSnapshot;

public record GetTodoSnapshotResult(
    Guid Id, 
    string Description, 
    TodoStatus Status
);