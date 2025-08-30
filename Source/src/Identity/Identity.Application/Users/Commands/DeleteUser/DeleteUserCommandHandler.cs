using MediatR;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepo;

        public DeleteUserCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.Id,cancellationToken);

            if (user == null)
                throw new Exception("User not found.");

            user.DeleteUser();
            await _userRepo.UpdateAsync(user,cancellationToken);

            return true;
        }
    }
}
