using MediatR;

namespace Identity.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string UserName,
        int Age,
        string Email,
        string Password,
        Guid RoleId
    ) : IRequest<Guid>;
}
