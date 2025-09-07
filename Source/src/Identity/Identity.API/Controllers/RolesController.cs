using Identity.Application.Roles.Commands.CreateRole;
using Identity.Application.Roles.Commands.UpdateRole;
using Identity.Application.Roles.Commands.DeleteRole;
using Identity.Application.Roles.Queries.GetRoleById;
using Identity.Application.Roles.Queries.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));
            if (role == null)
                return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var roleId = await _mediator.Send(command);
            return Ok(roleId);
        }

        [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRoleCommand(id));
            return NoContent();
        }
    }
}
