using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor;
using AppAcademy.Application.Features.Proveedores.Commands.DeleteProveedor;
using AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetAllProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetProveedorById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProveedorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllProveedores")]
        public async Task<ActionResult<IEnumerable<GetAllProveedoresVm>>> GetAllProvedores()
        {
            try
            {
                var query = new GetAllProveedoresListQuery();
                var proveedores = await _mediator.Send(query);

                if(proveedores == null || !proveedores.Any())
                {
                    return NotFound("No se encontraron proveedores.");
                }

                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetProveedorById
        [HttpGet("GetProveedorById/{id}")]
        public async Task<ActionResult<GetProveedorByIdVm>> GetProveedorById(string id)
        {
            try
            {
                var command = new GetProveedorByIdQuery(id);

                var proveedor = await _mediator.Send(command);

                if (proveedor == null)
                {
                    return NotFound();
                }

                return Ok(proveedor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Proveedor con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateProveedor
        [HttpPost("CreateProveedor")]
        public async Task<ActionResult<string>> CreateProveedor([FromBody] CreateProveedorCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region UpdateProveedor
        [HttpPut("UpdateProveedor")]
        public async Task<ActionResult> UpdateProveedor([FromBody] UpdateProveedorCommand command)
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

        #region DeleteProveedor
        [HttpDelete("DeleteProveedor/{id}")]
        public async Task<ActionResult> DeleteProveedor(string id)
        {
            try
            {
                var command = new DeleteProveedorCommand
                {
                    ProveedorId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Proveedor con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
