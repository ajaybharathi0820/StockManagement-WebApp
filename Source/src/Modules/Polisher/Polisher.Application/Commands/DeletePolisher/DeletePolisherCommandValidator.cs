using FluentValidation;

namespace Polisher.Application.Commands.DeletePolisher;

public class DeletePolisherCommandValidator : AbstractValidator<DeletePolisherCommand>
{
    public DeletePolisherCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Polisher ID is required.");
    }
}
