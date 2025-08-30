using FluentValidation;

namespace BagType.Application.Commands.DeleteBagType
{
    public class DeleteBagTypeCommandValidator : AbstractValidator<DeleteBagTypeCommand>
    {
        public DeleteBagTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
