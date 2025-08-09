using MediatR;
using Microsoft.AspNetCore.Mvc;
using Polisher.Application.Commands.CreatePolisher;

namespace Polisher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolisherController : ControllerBase
{
    private readonly IMediator _mediator;

    public PolisherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePolisherCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
