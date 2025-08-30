using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Users.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IUserRepository _userRepo;

        public ForgotPasswordCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // var user = await _userRepo.GetByEmailAsync(request.Email);
            // if (user == null) throw new Exception("User not found");

            var token = Guid.NewGuid().ToString("N"); // simple token, in real case use JWT/email
            // Save token to DB or cache (not shown)

            return token;
        }
    }
}
