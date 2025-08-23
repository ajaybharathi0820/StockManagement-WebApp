using MediatR;
using Polisher.Application.DTOs;

namespace Polisher.Application.Commands.UpdatePolisher;

public class UpdatePolisherCommand : IRequest<bool>
{
    public PolisherDTO Polisher { get; set; }
}
