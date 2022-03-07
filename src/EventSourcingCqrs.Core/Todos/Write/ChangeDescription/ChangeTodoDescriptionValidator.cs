using FluentValidation;

namespace EventSourcingCqrs.Core.Todos.Write.ChangeDescription;

public class ChangeTodoDescriptionValidator : 
    AbstractValidator<ChangeTodoDescriptionCommand>
{
    public ChangeTodoDescriptionValidator()
    {
        RuleFor(e => e.Description)
            .MinimumLength(2)
            .MaximumLength(255);
    }
}