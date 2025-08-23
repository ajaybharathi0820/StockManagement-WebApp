using FluentValidation;

namespace Polisher.Application.Queries.GetPolisherById;

public class GetPolisherByIdQueryValidator : AbstractValidator<GetPolisherByIdQuery>
{
    public GetPolisherByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Polisher ID is required.");
    }
}
