using MediatR;
using Polisher.Application.DTOs;

namespace Polisher.Application.Queries.GetAllPolishers;

public class GetAllPolishersQuery : IRequest<IEnumerable<PolisherDTO>>
{
}
