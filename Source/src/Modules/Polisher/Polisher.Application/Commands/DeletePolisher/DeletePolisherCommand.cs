using MediatR;

namespace Polisher.Application.Commands.DeletePolisher;

public class DeletePolisherCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
