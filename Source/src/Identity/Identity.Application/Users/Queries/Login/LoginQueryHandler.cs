using MediatR;
using Microsoft.EntityFrameworkCore;
using Identity.Domain.Auth;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUserRepository userRepo, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepo = userRepo;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByUsernameAsync(request.UserName,cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password,user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");


            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                UserName = user.UserName,
                Role = user.Role.Name
            };
        }
    }
}
