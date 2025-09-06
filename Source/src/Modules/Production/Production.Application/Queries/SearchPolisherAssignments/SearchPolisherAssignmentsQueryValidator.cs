using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Production.Application.Queries.SearchPolisherAssignments
{
    public class SearchPolisherAssignmentsQueryValidator : AbstractValidator<SearchPolisherAssignmentsQuery>
    {
        public SearchPolisherAssignmentsQueryValidator()
        {
            RuleFor(x => x.Criteria).NotNull().WithMessage("Search criteria is required.");

            RuleFor(x => x.Criteria.FromDate)
                .LessThanOrEqualTo(x => x.Criteria.ToDate)
                .When(x => x.Criteria.FromDate.HasValue && x.Criteria.ToDate.HasValue)
                .WithMessage("FromDate must be less than or equal to ToDate.");
        }
    }
}