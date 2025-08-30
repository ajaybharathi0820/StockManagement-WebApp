using MediatR;

namespace Identity.Application.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : IRequest<bool>;
}
