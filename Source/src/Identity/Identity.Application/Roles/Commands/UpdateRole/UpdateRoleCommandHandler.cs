using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand,bool>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id,cancellationToken);

        if (role == null)
            throw new Exception("Role not found");

        role.UpdateRole(request.Name);
        await _roleRepository.UpdateAsync(role,cancellationToken);

        return true;
    }
}
