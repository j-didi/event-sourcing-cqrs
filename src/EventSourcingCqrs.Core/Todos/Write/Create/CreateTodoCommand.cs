using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.Create;

public record CreateTodoCommand(string Description): 
    IRequest<CreateTodoResult>;