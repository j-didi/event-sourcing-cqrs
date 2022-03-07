using FluentValidation;

namespace EventSourcingCqrs.Core.Todos.Write.Create;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(e => e.Description)
            .MinimumLength(2)
            .MaximumLength(255);
    }
}