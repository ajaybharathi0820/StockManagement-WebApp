using MediatR;
using Polisher.Application.DTOs;

namespace Polisher.Application.Commands.CreatePolisher;

public class CreatePolisherCommand : IRequest<Guid>
{
    public PolisherDTO Polisher { get; set; }
    // For auditing
    public string? CurrentUserId { get; set; }
}