using MediatR;
using Polisher.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Polisher.Application.Commands.DeletePolisher;

public class DeletePolisherCommandHandler : IRequestHandler<DeletePolisherCommand, bool>
{
    private readonly IPolisherRepository _polisherRepository;
    private readonly ILogger<DeletePolisherCommandHandler> _logger;

    public DeletePolisherCommandHandler(IPolisherRepository polisherRepository, ILogger<DeletePolisherCommandHandler> logger)
    {
        _polisherRepository = polisherRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePolisherCommand request, CancellationToken cancellationToken)
    {
        var polisher = await _polisherRepository.GetByIdAsync(request.Id, cancellationToken);
        if (polisher == null)
        {
            _logger.LogWarning("Polisher with ID {Id} not found.", request.Id);
            throw new Exception("Polisher not found.");
        }

        await _polisherRepository.DeleteAsync(polisher.Id, cancellationToken);
        _logger.LogInformation("Polisher {Id} deleted successfully.", polisher.Id);

        return true;
    }
}
