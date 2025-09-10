// Application/Commands/UpdatePolisher/UpdatePolisherCommandValidator.cs
using FluentValidation;
using Polisher.Domain.Repositories;

namespace Polisher.Application.Commands.UpdatePolisher;

public class UpdatePolisherCommandValidator : AbstractValidator<UpdatePolisherCommand>
{
    private readonly IPolisherRepository _polisherRepository;

    public UpdatePolisherCommandValidator(IPolisherRepository polisherRepository)
    {
        _polisherRepository = polisherRepository;

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
            .Matches(@"^\d{10}$").WithMessage("Contact number must be 10 digits.")
            .MustAsync(async (command, contactNumber, cancellation) => !await _polisherRepository.IsContactNumberExistsAsync(contactNumber, command.Polisher.Id, cancellation))
            .WithMessage("Contact number already exists. Please choose a different contact number.");

        RuleFor(x => x.Polisher)
            .MustAsync(async (command, polisher, cancellation) => !await _polisherRepository.IsNameCombinationExistsAsync(polisher.FirstName, polisher.LastName, polisher.Id, cancellation))
            .WithMessage("A polisher with the same first name and last name combination already exists.");
    }
}
