using EventSourcingCqrs.SharedKernel.DataContracts;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.UndoToVersion;

public record UndoTodoStateToVersionCommand(
    Guid EntityId, 
    Guid EventId
): IRequest<EmptyResult>;