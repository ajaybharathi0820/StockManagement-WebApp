using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Polisher.Application.Commands.CreatePolisher;
using Polisher.Application.Commands.DeletePolisher;
using Polisher.Application.Commands.UpdatePolisher;
using Polisher.Application.Queries.GetPolisherById;
using Polisher.Application.Queries.GetAllPolishers;

namespace Polisher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class PolisherController : ControllerBase
{
    private readonly IMediator _mediator;

    public PolisherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all polishers
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPolishersQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get polisher by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPolisherByIdQuery { Id = id });
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Create a new polisher
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePolisherCommand command)
    {
        var createdId = await _mediator.Send(command);
        return Ok(createdId);
    }

    /// <summary>
    /// Update an existing polisher
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePolisherCommand command)
    {
        if (id != command.Polisher.Id)
            return BadRequest("ID in URL and body do not match");

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete a polisher
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePolisherCommand { Id = id });
        return NoContent();
    }

}
