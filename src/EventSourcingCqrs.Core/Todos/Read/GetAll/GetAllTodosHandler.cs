using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.SharedKernel.DataContracts;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetAll;

public class GetAllTodosHandler: 
    IRequestHandler<EmptyQuery<GetAllTodosResult>, GetAllTodosResult>
{
    private readonly ITodosPort _todos;

    public GetAllTodosHandler(ITodosPort todos)
    {
        _todos = todos;
    }

    public async Task<GetAllTodosResult> Handle(
        EmptyQuery<GetAllTodosResult> request,
        CancellationToken cancellationToken
    )
    {
        var todos = await _todos.GetAll();
        var items = todos
            .Select(e => new GetAllTodosResult.GetAllTodosItem(e.Id, e.Description, e.Status))
            .ToList();

        return new GetAllTodosResult(items);
    }
}