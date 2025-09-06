namespace Identity.Application.Roles.Commands.UpdateRole;

using MediatR;

public record UpdateRoleCommand(Guid Id, string Name) : IRequest<bool>;
