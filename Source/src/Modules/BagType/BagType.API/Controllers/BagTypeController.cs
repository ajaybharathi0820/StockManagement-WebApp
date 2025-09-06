using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BagType.Application.Commands.CreateBagType;
using BagType.Application.Commands.UpdateBagType;
using BagType.Application.Commands.DeleteBagType;
using BagType.Application.Queries.GetBagTypeById;
using BagType.Application.Queries.GetBagTypes;

namespace BagType.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // 🔐 Require authentication, same as Identity + Polisher
    public class BagTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BagTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all bag types
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetBagTypesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get a bag type by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBagTypeByIdQuery(id) );
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Create a new bag type
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBagTypeCommand command)
        {
            var createdId = await _mediator.Send(command);
            return Ok(createdId);
        }

        /// <summary>
        /// Update an existing bag type
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBagTypeCommand command)
        {
            if (id != command.BagType.Id)
                return BadRequest("ID in URL and body do not match");

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a bag type
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteBagTypeCommand { Id = id });
            return NoContent();
        }

    }
}