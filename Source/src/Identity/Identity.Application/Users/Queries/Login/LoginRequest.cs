using MediatR;
namespace Identity.Application.Users.Queries.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
