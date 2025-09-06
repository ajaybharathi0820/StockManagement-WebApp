using FluentValidation;

namespace BagType.Application.Commands.DeleteBagType
{
    public class DeleteBagTypeCommandValidator : AbstractValidator<DeleteBagTypeCommand>
    {
        public DeleteBagTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id must not be empty");
        }
    }
}
