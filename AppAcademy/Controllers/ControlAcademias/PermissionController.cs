using AppAcademy.Application.Features.Permissions.Commands.CreatePermission;
using AppAcademy.Application.Features.Permissions.Commands.DeletePermission;
using AppAcademy.Application.Features.Permissions.Commands.UpdatePermission;
using AppAcademy.Application.Features.Permissions.Queries.GetAllPermissions;
using AppAcademy.Application.Features.Permissions.Queries.GetPermission;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreatePermissionCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(new { message = "Permiso creado exitosamente.", careerId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdatePermissionCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var command = new DeletePermissionCommand
                {
                    PermissionId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Permiso con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllPermissionsVm>>> GetAll()
        {
            try
            {
                var query = new GetAllPermissionsListQuery();
                var permissions = await _mediator.Send(query);

                if (permissions == null || !permissions.Any())
                {
                    return NotFound("No se encontraron permisos.");
                }

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetPermissionVm>> GetById(int id)
        {
            try
            {
                var command = new GetPermissionQuery(id);

                var permission = await _mediator.Send(command);

                if (permission == null)
                {
                    return NotFound();
                }

                return Ok(permission);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Permiso con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
