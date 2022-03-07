using EventSourcingCqrs.Core.Ports.Repository;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using MediatR;

namespace EventSourcingCqrs.Core.Todos.Read.GetById;

public class GetTodoByIdHandler : 
    IRequestHandler<GetTodoByIdQuery, GetTodoByIdResult>
{
    private readonly IDomainValidationPort _domainValidator;
    private readonly ITodosPort _todos;

    public GetTodoByIdHandler(
        IDomainValidationPort domainValidator,
        ITodosPort todos
    )
    {
        _domainValidator = domainValidator;
        _todos = todos;
    }
    
    public async Task<GetTodoByIdResult> Handle(
        GetTodoByIdQuery query, 
        CancellationToken cancellationToken
    )
    {
        var todo = await _todos.GetById(query.Id);

        if (todo == null)
        {
            _domainValidator.AddNotFound();
            return null;
        }
        
        return new GetTodoByIdResult(todo.Description, todo.Status);
    }
}