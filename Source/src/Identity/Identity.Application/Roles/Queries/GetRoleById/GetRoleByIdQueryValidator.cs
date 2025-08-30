using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Identity.Application.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryValidator: AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Role ID is required.");
        }
    }
}