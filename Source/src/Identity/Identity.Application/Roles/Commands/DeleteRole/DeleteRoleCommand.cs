namespace Identity.Application.Roles.Commands.DeleteRole;

using MediatR;

public record DeleteRoleCommand(int Id) : IRequest<bool>;
