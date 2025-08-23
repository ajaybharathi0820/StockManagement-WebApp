using MediatR;
using Polisher.Domain.Repositories;
using Polisher.Application.DTOs;

namespace Polisher.Application.Queries.GetPolisherById;

public class GetPolisherByIdQueryHandler : IRequestHandler<GetPolisherByIdQuery, PolisherDTO>
{
    private readonly IPolisherRepository _polisherRepository;

    public GetPolisherByIdQueryHandler(IPolisherRepository polisherRepository)
    {
        _polisherRepository = polisherRepository;
    }

    public async Task<PolisherDTO> Handle(GetPolisherByIdQuery request, CancellationToken cancellationToken)
    {
        var polisher = await _polisherRepository.GetByIdAsync(request.Id, cancellationToken);
        if (polisher == null) return null;

        return new PolisherDTO
        {
            Id = polisher.Id,
            FirstName = polisher.FirstName,
            LastName = polisher.LastName,
            ContactNumber = polisher.ContactNumber
        };
    }
}
