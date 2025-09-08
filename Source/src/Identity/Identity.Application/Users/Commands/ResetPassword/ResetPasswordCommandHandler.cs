using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepo;
        public ResetPasswordCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found");

            user.ChangePassword(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));
            await _userRepo.UpdateAsync(user, cancellationToken);
            return true;
        }
    }
}
