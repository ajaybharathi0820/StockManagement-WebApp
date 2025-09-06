using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Production.Application.DTOs;

namespace Production.Application.Commands.CreatePolisherAssignment
{
    public class PolisherAssignmentItemDtoValidator: AbstractValidator<PolisherAssignmentItemDto>
    {
        public PolisherAssignmentItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("ProductCode is required.")
                .MaximumLength(50);

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName is required.")
                .MaximumLength(100);

            RuleFor(x => x.BagTypeId)
                .NotEmpty().WithMessage("BagTypeId is required.");

            RuleFor(x => x.BagTypeName)
                .NotEmpty().WithMessage("BagTypeName is required.")
                .MaximumLength(50);

            RuleFor(x => x.BagWeight)
                .GreaterThan(0).WithMessage("BagWeight must be greater than 0.");

            RuleFor(x => x.Dozens)
                .GreaterThan(0).WithMessage("Dozens must be greater than 0.");

            RuleFor(x => x.TotalWeight)
                .GreaterThan(0).WithMessage("TotalWeight must be greater than 0.");

            RuleFor(x => x.NetWeight)
                .GreaterThanOrEqualTo(0).WithMessage("NetWeight must be non-negative.");

            RuleFor(x => x.AvgWeight)
                .GreaterThanOrEqualTo(0).WithMessage("AvgWeight must be non-negative.");

            RuleFor(x => x.ProductAvgWeight)
                .GreaterThanOrEqualTo(0).WithMessage("ProductAvgWeight must be non-negative.");

            RuleFor(x => x.ToleranceDiff)
                .NotEmpty().WithMessage("ToleranceDiff is required.");
        }
    }
}