using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Production.Application.Queries.GetPolisherAssignmentById
{
    public class GetPolisherAssignmentByIdQueryValidator: AbstractValidator<GetPolisherAssignmentByIdQuery>
    {
        public GetPolisherAssignmentByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id must be greater than 0");
        }
    }
}