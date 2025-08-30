using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand,bool>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id,cancellationToken);

        if (role == null)
            throw new Exception("Role not found");

        role.DeleteRole();
        await _roleRepository.UpdateAsync(role,cancellationToken);

        return true;
    }
}
