using FluentValidation;

namespace BagType.Application.Queries.GetBagTypeById
{
    public class GetBagTypeByIdQueryValidator : AbstractValidator<GetBagTypeByIdQuery>
    {
        public GetBagTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("BagType Id must be greater than 0.");
        }
    }
}