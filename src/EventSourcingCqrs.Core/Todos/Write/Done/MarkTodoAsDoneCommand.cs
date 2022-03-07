using EventSourcingCqrs.SharedKernel.DataContracts;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.Done;

public record MarkTodoAsDoneCommand(Guid Id): IRequest<EmptyResult>;