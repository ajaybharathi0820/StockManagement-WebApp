using Identity.Domain.Repositories;
using MediatR;
using Identity.Application.Roles.DTOs;

namespace Identity.Application.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id,cancellationToken);

        if (role == null)
            throw new Exception("Role not found");

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
        };
    }
}
