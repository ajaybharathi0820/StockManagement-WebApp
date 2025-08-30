namespace Identity.Application.Roles.Commands.UpdateRole;

using MediatR;

public record UpdateRoleCommand(int Id, string Name) : IRequest<bool>;
