using FluentValidation;

namespace BagType.Application.Queries.GetBagTypeById
{
    public class GetBagTypeByIdQueryValidator : AbstractValidator<GetBagTypeByIdQuery>
    {
        public GetBagTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("BagType Id must not be empty.");
        }
    }
}