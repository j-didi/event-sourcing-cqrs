using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcingCqrs.Core.Todos.Read.GetById;

public record GetTodoByIdResult(string Description, TodoStatus Status);