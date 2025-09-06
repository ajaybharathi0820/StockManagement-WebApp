using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Identity.Application.Roles.Commands.CreateRole
{
    public record CreateRoleCommand(string Name) : IRequest<Guid>;
}