using AppAcademy.Application.Features.Auth.Roles.Commands.CreateRol;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRolWithPermissions([FromBody] CreateRolCommand command)
        {
            await _mediator.Send(command);
            return Ok("Rol creado exitosamente");
        }
    }
}
