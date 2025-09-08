using MediatR;

namespace Identity.Application.Users.Commands.ResetPassword
{
    public record ResetPasswordCommand(Guid UserId, string NewPassword, string ConfirmPassword) : IRequest<bool>;
}
