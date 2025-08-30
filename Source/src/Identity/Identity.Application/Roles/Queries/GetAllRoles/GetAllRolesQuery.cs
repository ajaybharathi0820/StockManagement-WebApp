namespace Identity.Application.Roles.Queries.GetAllRoles;

using MediatR;
using Identity.Application.Roles.DTOs;

public record GetAllRolesQuery() : IRequest<IEnumerable<RoleDto>>;

