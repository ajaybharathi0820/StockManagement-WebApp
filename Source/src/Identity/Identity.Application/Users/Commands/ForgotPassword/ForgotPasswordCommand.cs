using MediatR;

namespace Identity.Application.Users.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest<string>;
}
