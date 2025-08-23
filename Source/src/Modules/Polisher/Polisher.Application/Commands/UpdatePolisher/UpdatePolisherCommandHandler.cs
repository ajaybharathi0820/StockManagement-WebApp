using MediatR;
using Polisher.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Polisher.Application.Commands.UpdatePolisher;

public class UpdatePolisherCommandHandler : IRequestHandler<UpdatePolisherCommand, bool>
{
    private readonly IPolisherRepository _polisherRepository;
    private readonly ILogger<UpdatePolisherCommandHandler> _logger;

    public UpdatePolisherCommandHandler(IPolisherRepository polisherRepository, ILogger<UpdatePolisherCommandHandler> logger)
    {
        _polisherRepository = polisherRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdatePolisherCommand request, CancellationToken cancellationToken)
    {
        var polisher = await _polisherRepository.GetByIdAsync(request.Polisher.Id, cancellationToken);
        if (polisher == null)
        {
            _logger.LogWarning("Polisher with ID {Id} not found.", request.Polisher.Id);
            throw new Exception("Polisher not found.");
        }

        polisher.Update(request.Polisher.FirstName, request.Polisher.LastName, request.Polisher.ContactNumber);

        await _polisherRepository.UpdateAsync(polisher, cancellationToken);
        _logger.LogInformation("Polisher {Id} updated successfully.", polisher.Id);

        return true;
    }
}
