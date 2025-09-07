using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using MediatR;
using BCrypt.Net;

namespace Identity.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;

        public CreateUserCommandHandler(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepo.GetByIdAsync(request.RoleId,cancellationToken);
            if (role == null)
                throw new Exception("Invalid role");

            var user = new User(
                request.FirstName,
                request.LastName,
                request.UserName,
                request.Age,
                request.Email,
                BCrypt.Net.BCrypt.HashPassword(request.Password),
                role.Id
            );

            // Audit
            var createdBy = !string.IsNullOrWhiteSpace(request.CurrentUserId) ? request.CurrentUserId! : "System";
            user.MarkCreated(createdBy);

            await _userRepo.AddAsync(user,cancellationToken);
            return user.Id;
        }
    }
}
