using EventSourcingCqrs.SharedKernel.DataContracts;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Write.ChangeDescription;

public record ChangeTodoDescriptionCommand(
    Guid Id,
    string Description
): IRequest<EmptyResult>;