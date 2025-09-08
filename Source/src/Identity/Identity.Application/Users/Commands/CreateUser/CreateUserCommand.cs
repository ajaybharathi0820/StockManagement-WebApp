using MediatR;

namespace Identity.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string UserName,
    DateTime DateOfBirth,
        string Email,
        string Password,
        Guid RoleId
    ) : IRequest<Guid>
    {
        // Populated by controller from the authenticated user's claims; not from client input
        public string? CurrentUserId { get; set; }
    }
}
