using MediatR;
using Polisher.Domain.Repositories;
using Polisher.Application.DTOs;

namespace Polisher.Application.Queries.GetAllPolishers;

public class GetAllPolishersQueryHandler : IRequestHandler<GetAllPolishersQuery, IEnumerable<PolisherDTO>>
{
    private readonly IPolisherRepository _polisherRepository;

    public GetAllPolishersQueryHandler(IPolisherRepository polisherRepository)
    {
        _polisherRepository = polisherRepository;
    }

    public async Task<IEnumerable<PolisherDTO>> Handle(GetAllPolishersQuery request, CancellationToken cancellationToken)
    {
        var polishers = await _polisherRepository.GetAllAsync(cancellationToken);

        return polishers.Select(p => new PolisherDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            ContactNumber = p.ContactNumber
        });
    }
}
