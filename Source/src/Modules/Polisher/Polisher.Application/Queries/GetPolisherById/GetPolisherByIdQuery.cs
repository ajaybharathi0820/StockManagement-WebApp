using MediatR;
using Polisher.Application.DTOs;

namespace Polisher.Application.Queries.GetPolisherById;

public class GetPolisherByIdQuery : IRequest<PolisherDTO>
{
    public Guid Id { get; set; }
}
