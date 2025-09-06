namespace Identity.Application.Roles.Queries.GetRoleById;

using MediatR;
using Identity.Application.Roles.DTOs;

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleDto>;

