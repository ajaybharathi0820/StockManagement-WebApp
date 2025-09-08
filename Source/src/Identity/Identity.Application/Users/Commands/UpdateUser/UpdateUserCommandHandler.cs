using MediatR;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;

        public UpdateUserCommandHandler(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.Id,cancellationToken);

            if (user == null)
                throw new Exception("User not found.");

            var role = await _roleRepo.GetByIdAsync(request.RoleId,cancellationToken);
            if (role == null)
                throw new Exception("Invalid role");

            user.UpdateUser(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.Email,
                role.Id
            );

            // Audit
            var updatedBy = !string.IsNullOrWhiteSpace(request.CurrentUserId) ? request.CurrentUserId! : "System";
            user.MarkUpdated(updatedBy);

            await _userRepo.UpdateAsync(user,cancellationToken);

            return true;
        }
    }
}
