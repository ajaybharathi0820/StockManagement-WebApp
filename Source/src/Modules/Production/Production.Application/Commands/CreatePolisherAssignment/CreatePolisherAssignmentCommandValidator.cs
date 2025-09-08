using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Production.Application.Commands.CreatePolisherAssignment
{
    public class CreatePolisherAssignmentCommandValidator : AbstractValidator<CreatePolisherAssignmentCommand>
    {
        public CreatePolisherAssignmentCommandValidator()
        {
            RuleFor(x => x.polisherAssignment)
                .NotNull().WithMessage("PolisherAssignment cannot be null.");

            When(x => x.polisherAssignment != null, () =>
            {
                RuleFor(x => x.polisherAssignment.PolisherId)
                    .NotEmpty().WithMessage("PolisherId is required.");

                RuleFor(x => x.polisherAssignment.PolisherName)
                .NotEmpty().WithMessage("PolisherName is required.")
                .MaximumLength(100);
                
                RuleForEach(x => x.polisherAssignment.Items)
                    .SetValidator(new PolisherAssignmentItemDtoValidator());
            });
        }
    }
}