using FluentValidation;
using FluentValidation.AspNetCore;

namespace Polisher.Application.Commands.CreatePolisher;

public class CreatePolisherCommandValidator : AbstractValidator<CreatePolisherCommand>
{
    public CreatePolisherCommandValidator()
    {
        RuleFor(x => x.polisher.FirstName)
            .NotEmpty().WithMessage("Polisher first name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("First name must contain only letters.")
            .MaximumLength(100).WithMessage("Polisher first name must be less than 100 characters.");

        RuleFor(x => x.polisher.LastName)
            .NotEmpty().WithMessage("Polisher last name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("Last name must contain only letters.")
            .MaximumLength(100).WithMessage("Polisher last name must be less than 100 characters.");

        RuleFor(x => x.polisher.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\d{10}$").WithMessage("Contact number must be 10 digits.");
    }
}