using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BagType.Application.Commands.UpdateBagType
{
    public class UpdateBagTypeCommandValidator : AbstractValidator<UpdateBagTypeCommand>
    {
        public UpdateBagTypeCommandValidator()
        {
            RuleFor(x => x.BagType.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.BagType.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.BagType.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");
        }
    }
}