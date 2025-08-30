using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Product.Application.Queries.GetProductByCode
{
    public class GetProductByCodeValidator : AbstractValidator<GetProductByCodeQuery>
    {
        public GetProductByCodeValidator()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("ProductCode is required")
                .MaximumLength(50).WithMessage("ProductCode cannot exceed 50 characters");
        }
    }
}