using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetById;

public record GetTodoByIdQuery(Guid Id): IRequest<GetTodoByIdResult>;