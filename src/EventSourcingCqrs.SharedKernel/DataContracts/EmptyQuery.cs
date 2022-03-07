using MediatR;

namespace EventSourcingCqrs.SharedKernel.DataContracts;

public record EmptyQuery<T>: IRequest<T>;