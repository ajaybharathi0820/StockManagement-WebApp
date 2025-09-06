namespace Identity.Application.Roles.Commands.DeleteRole;

using MediatR;

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;
