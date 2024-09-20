using AppAcademy.Application.Features.Cortes.Queries.GetCorte;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes;
using AppAcademy.Application.Features.DetallesPagos.Command.CreateDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Command.DeleteDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Command.UpdateDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetDetallePago;
using AppAcademy.Application.Features.Devoluciones.Command.CreateDevolucion;
using AppAcademy.Application.Features.Devoluciones.Command.DeleteDevolucion;
using AppAcademy.Application.Features.Devoluciones.Command.UpdateDevolucion;
using AppAcademy.Application.Features.Devoluciones.Queries.GetAllDevoluciones;
using AppAcademy.Application.Features.Devoluciones.Queries.GetDevolucion;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DevolucionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevolucionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region GetAll
        [HttpGet("GetAllDevoluciones")]
        public async Task<ActionResult<IEnumerable<GetAllDevolucionesVm>>> GetAllDevoluciones()
        {
            try
            {
                var query = new GetAllDevolucionesListQuery();
                var devoluciones = await _mediator.Send(query);

                if (devoluciones == null || !devoluciones.Any())
                {
                    return NotFound("No se encontraron devoluciones.");
                }

                return Ok(devoluciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetDevolucionById
        [HttpGet("GetDevolucionById/{id}")]
        public async Task<ActionResult<GetDevolucionVm>> GetDevolucionById(string id)
        {
            try
            {
                var command = new GetDevolucionQuery(id);

                var devolucion = await _mediator.Send(command);

                if (devolucion == null)
                {
                    return NotFound();
                }

                return Ok(devolucion);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Devolucion con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateDevolucion
        [HttpPost("CreateDevolucion")]
        public async Task<ActionResult<string>> CreateDevolucion([FromBody] CreateDevolucionCommand command)
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

        #region UpdateDevolucion
        [HttpPut("UpdateDevolucion")]
        public async Task<ActionResult> UpdateDevolucion([FromBody] UpdateDevolucionCommand command)
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

        #region DeleteDevolucion
        [HttpDelete("DeleteDevolucion/{id}")]
        public async Task<ActionResult> DeleteDevolucion(string id)
        {
            try
            {
                var command = new DeleteDevolucionCommand
                {
                    DevolucionId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Devolucion con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
