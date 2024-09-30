using AppAcademy.Application.DTOs;
using AppAcademy.Application.Features.Auth.Users.Commands.CreateUser;
using AppAcademy.Application.Features.Auth.Users.Commands.LoginUser;
using AppAcademy.Application.Features.Auth.Users.Commands.RefreshTokenUser;
using AppAcademy.Domain.Auth;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.AuthControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserCommand> _createValidator;
        private readonly IValidator<LoginUserCommand> _loginValidator;

        public UserController(IMediator mediator, IValidator<CreateUserCommand> createValidator, IValidator<LoginUserCommand> loginValidator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            // Validar el comando
            var validationResult = await _createValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var user = await _mediator.Send(command);
                return CreatedAtAction(nameof(Register), user);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409 Conflict si el usuario ya existe
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var validationResult = await _loginValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var user = await _mediator.Send(command);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); // 401 Unauthorized si hay problemas de autenticación
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.InnerException}");
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<UserDto>> RefreshToken([FromBody] RefreshTokenUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
