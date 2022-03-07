using EventSourcingCqrs.API.Common;
using EventSourcingCqrs.Core.Todos.Read.GetAll;
using EventSourcingCqrs.Core.Todos.Read.GetById;
using EventSourcingCqrs.Core.Todos.Read.GetEventsHistoryById;
using EventSourcingCqrs.Core.Todos.Read.GetTodoSnapshot;
using EventSourcingCqrs.Core.Todos.Write.ChangeDescription;
using EventSourcingCqrs.Core.Todos.Write.Create;
using EventSourcingCqrs.Core.Todos.Write.Done;
using EventSourcingCqrs.Core.Todos.Write.PutInProgress;
using EventSourcingCqrs.Core.Todos.Write.UndoToVersion;
using EventSourcingCqrs.SharedKernel.DataContracts;
using EventSourcingCqrs.SharedKernel.DomainValidations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingCqrs.API.Controllers;

public class TodoController: BaseController
{
    private readonly IMediator _mediator;

    public TodoController(
        IMediator mediator,
        IDomainValidationPort domainValidator
    ): base(domainValidator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) =>
        OkResult(await _mediator.Send(new GetTodoByIdQuery(id)));
    
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        OkResult(await _mediator.Send(new EmptyQuery<GetAllTodosResult>()));
    
    [HttpGet("{id:guid}/history")]
    public async Task<IActionResult> GetHistory([FromRoute] Guid id) =>
        OkResult(await _mediator.Send(new GetTodoEventsHistoryByIdQuery(id)));
    
    [HttpGet("{id:guid}/snapshot/{eventId:guid}")]
    public async Task<IActionResult> GetHistory(
        [FromRoute] Guid id,
        [FromRoute] Guid eventId
    ) => OkResult(await _mediator.Send(new GetTodoSnapshotQuery(id, eventId)));

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] CreateTodoCommand command) =>
        OkResult(await _mediator.Send(command));
    
    [HttpPut("change-description")]
    public async Task<IActionResult> ChangeDescription(
        [FromBody] ChangeTodoDescriptionCommand command
    ) => OkResult(await _mediator.Send(command));
    
    [HttpPut("{id:guid}/done")]
    public async Task<IActionResult> MarkAsDone([FromRoute] Guid id) => 
        OkResult(await _mediator.Send(new MarkTodoAsDoneCommand(id)));
    
    [HttpPut("{id:guid}/put-in-progress")]
    public async Task<IActionResult> MarkAsInProgress([FromRoute] Guid id) => 
        OkResult(await _mediator.Send(new MarkTodoAsInProgressCommand(id)));
    
    [HttpPut("{id:guid}/undo-to/{eventId:guid}")]
    public async Task<IActionResult> ChangeDescription(
        [FromRoute] Guid id,
        [FromRoute] Guid eventId
    ) => OkResult(await _mediator.Send(new UndoTodoStateToVersionCommand(id, eventId)));
}
