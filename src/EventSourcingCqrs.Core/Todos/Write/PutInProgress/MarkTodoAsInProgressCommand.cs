using EventSourcingCqrs.SharedKernel.DataContracts;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.PutInProgress;

public record MarkTodoAsInProgressCommand(Guid Id): IRequest<EmptyResult>;