using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepo;

        public ChangePasswordCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId,cancellationToken);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword,user.Password))
                throw new UnauthorizedAccessException("Old password is incorrect");
            user.ChangePassword(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));
            await _userRepo.UpdateAsync(user,cancellationToken);

            return true;
        }
    }
}
