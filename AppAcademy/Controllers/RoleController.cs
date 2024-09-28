using AppAcademy.Application.Features.Auth.Roles.Commands.CreatePermisionWithRole;
using AppAcademy.Application.Features.Auth.Roles.Commands.CreateRol;
using AppAcademy.Application.Features.Auth.Roles.Commands.DeleteRol;
using AppAcademy.Application.Features.Auth.Roles.Commands.EditRol;
using MediatR;
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

        [HttpPost]
        [Route("assign-permissions")]
        public async Task<IActionResult> AssignPermissionsToRole([FromBody] CreatePermisionWithRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok("Permisos asignados exitosamente");
        }

        [HttpPut]
        [Route("edit-role")]
        public async Task<IActionResult> EditRoleWithPermissions([FromBody] EditRolCommand command)
        {
            await _mediator.Send(command);
            return Ok("Rol actualizado correctamente");
        }

        [HttpDelete]
        [Route("delete-role/{roleId}")]
        public async Task<IActionResult> DeleteRole([FromRoute] string roleId)
        {
            await _mediator.Send(new DeleteRolCommand { RolId = roleId });
            return Ok("Rol eliminado exitosamente");
        }
    }
}
