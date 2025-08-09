using MediatR;
using Polisher.Application.DTOs;

namespace Polisher.Application.Commands.CreatePolisher;

public class CreatePolisherCommand : IRequest<Guid>
{
    public PolisherDTO polisher { get; set; }
}