using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Production.Application.DTOs;
using Production.Application.Commands.CreatePolisherAssignment;
using Production.Application.Queries.GetPolisherAssignmentById;
using Production.Application.Queries.SearchPolisherAssignments;
using System.Security.Claims;

namespace Production.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolisherAssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PolisherAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new Polisher Assignment
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePolisherAssignmentCommand command, CancellationToken cancellationToken)
        {
            if (command == null || command.polisherAssignment == null)
                return BadRequest("Invalid request payload.");

            command.CurrentUserId ??= User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdId = await _mediator.Send(command, cancellationToken);
            return Ok(createdId);
        }

        /// <summary>
        /// Get Polisher Assignment by Id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPolisherAssignmentByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Search Polisher Assignments
        /// </summary>
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchPolisherAssignmentsQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
                return BadRequest("Invalid request payload.");

            var results = await _mediator.Send(query, cancellationToken);
            return Ok(results);
        }
    }
}