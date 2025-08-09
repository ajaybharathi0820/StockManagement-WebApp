using MediatR;
using Polisher.Domain.Repositories;

namespace Polisher.Application.Commands.CreatePolisher;

public class CreatePolisherCommandHandler : IRequestHandler<CreatePolisherCommand, Guid>
{
    private readonly IPolisherRepository _polisherRepository;

    public CreatePolisherCommandHandler(IPolisherRepository polisherRepository)
    {
        _polisherRepository = polisherRepository;
    }

    public async Task<Guid> Handle(CreatePolisherCommand createRequest, CancellationToken cancellationToken)
    {
        var polisher = new Polisher.Domain.Entities.Polisher
        (
            Guid.NewGuid(),
            createRequest.polisher.FirstName,
            createRequest.polisher.LastName,
            createRequest.polisher.ContactNumber
        );

        await _polisherRepository.AddAsync(polisher, cancellationToken);
        return polisher.Id;
    }
}