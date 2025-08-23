// Application/Commands/UpdatePolisher/UpdatePolisherCommandValidator.cs
using FluentValidation;

namespace Polisher.Application.Commands.UpdatePolisher;

public class UpdatePolisherCommandValidator : AbstractValidator<UpdatePolisherCommand>
{
    public UpdatePolisherCommandValidator()
    {
        RuleFor(x => x.Polisher.Id)
            .NotEmpty().WithMessage("Polisher ID is required.");

        RuleFor(x => x.Polisher.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("First name must contain only letters.")
            .MaximumLength(100);

        RuleFor(x => x.Polisher.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("Last name must contain only letters.")
            .MaximumLength(100);

        RuleFor(x => x.Polisher.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\d{10}$").WithMessage("Contact number must be 10 digits.");
    }
}
