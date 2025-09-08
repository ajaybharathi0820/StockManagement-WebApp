using Identity.Application.Users.Commands.CreateUser;
using Identity.Application.Users.Commands.UpdateUser;
using Identity.Application.Users.Commands.DeleteUser;
using Identity.Application.Users.Commands.ChangePassword;
using Identity.Application.Users.Commands.ForgotPassword;
using Identity.Application.Users.Commands.ResetPassword;
using Identity.Application.Users.Queries.GetUserById;
using Identity.Application.Users.Queries.GetAllUsers;
using Identity.Application.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery{ Id = id });
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            // Set auditing
            command.CurrentUserId ??= User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");
            // Set auditing
            command.CurrentUserId ??= User?.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand{ Id = id });
            return NoContent();
        }

        [HttpPost("login")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest command)
        {
            var token = await _mediator.Send(command);
            if (string.IsNullOrEmpty(token.Token))
                return Unauthorized("Invalid credentials");

            return Ok(token);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password changed successfully");
        }

        [HttpPost("forgot-password")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password reset instructions sent");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            // Admin-triggered reset; no old password required
            await _mediator.Send(command);
            return Ok("Password reset successfully");
        }
    }
}
