using Identity.Domain.Repositories;
using MediatR;
using Identity.Application.Roles.DTOs;

namespace Identity.Application.Roles.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);
        return roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name
        });
    }
}
