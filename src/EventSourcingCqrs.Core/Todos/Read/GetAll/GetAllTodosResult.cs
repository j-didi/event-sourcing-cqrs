using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcingCqrs.Core.Todos.Read.GetAll;

public record GetAllTodosResult(List<GetAllTodosResult.GetAllTodosItem> Items)
{
    public record GetAllTodosItem(
        Guid Id, 
        string Description, 
        TodoStatus TodoStatus
    );
}