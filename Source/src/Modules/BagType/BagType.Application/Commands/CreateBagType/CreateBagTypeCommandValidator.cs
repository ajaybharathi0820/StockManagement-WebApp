using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BagType.Application.Commands.CreateBagType
{
    public class CreateBagTypeCommandValidator : AbstractValidator<CreateBagTypeCommand>
    {
        public CreateBagTypeCommandValidator()
        {
            RuleFor(x => x.BagType.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.BagType.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");
        }
    }
}