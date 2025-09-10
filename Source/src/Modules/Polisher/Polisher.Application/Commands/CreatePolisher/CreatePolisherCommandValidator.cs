using FluentValidation;
using FluentValidation.AspNetCore;
using Polisher.Domain.Repositories;

namespace Polisher.Application.Commands.CreatePolisher;

public class CreatePolisherCommandValidator : AbstractValidator<CreatePolisherCommand>
{
    private readonly IPolisherRepository _polisherRepository;

    public CreatePolisherCommandValidator(IPolisherRepository polisherRepository)
    {
        _polisherRepository = polisherRepository;

        RuleFor(x => x.Polisher.FirstName)
            .NotEmpty().WithMessage("Polisher first name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("First name must contain only letters.")
            .MaximumLength(100).WithMessage("Polisher first name must be less than 100 characters.");

        RuleFor(x => x.Polisher.LastName)
            .NotEmpty().WithMessage("Polisher last name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("Last name must contain only letters.")
            .MaximumLength(100).WithMessage("Polisher last name must be less than 100 characters.");

        RuleFor(x => x.Polisher.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\d{10}$").WithMessage("Contact number must be 10 digits.")
            .MustAsync(async (contactNumber, cancellation) => !await _polisherRepository.IsContactNumberExistsAsync(contactNumber, null, cancellation))
            .WithMessage("Contact number already exists. Please choose a different contact number.");

        RuleFor(x => x.Polisher)
            .MustAsync(async (polisher, cancellation) => !await _polisherRepository.IsNameCombinationExistsAsync(polisher.FirstName, polisher.LastName, null, cancellation))
            .WithMessage("A polisher with the same first name and last name combination already exists.");
    }
}