using MediatR;
using Polisher.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Polisher.Application.Commands.CreatePolisher;

public class CreatePolisherCommandHandler : IRequestHandler<CreatePolisherCommand, Guid>
{
    private readonly IPolisherRepository _polisherRepository;
    private readonly ILogger<CreatePolisherCommandHandler> _logger;

    public CreatePolisherCommandHandler(IPolisherRepository polisherRepository, ILogger<CreatePolisherCommandHandler> logger)
    {
        _polisherRepository = polisherRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreatePolisherCommand createRequest, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating polisher");
        var polisher = new Polisher.Domain.Entities.Polisher
        (
            Guid.NewGuid(),
            createRequest.Polisher.FirstName,
            createRequest.Polisher.LastName,
            createRequest.Polisher.ContactNumber
        );

        await _polisherRepository.AddAsync(polisher, cancellationToken);
        _logger.LogInformation("Polisher created successfully");
        return polisher.Id;
    }
}