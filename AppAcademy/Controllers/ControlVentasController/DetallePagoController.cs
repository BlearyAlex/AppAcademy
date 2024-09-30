using AppAcademy.Application.Features.Cortes.Queries.GetCorte;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes;
using AppAcademy.Application.Features.DetallesPagos.Command.CreateDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Command.DeleteDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Command.UpdateDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetDetallePago;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DetallePagoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DetallePagoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllDetallesPagos")]
        public async Task<ActionResult<IEnumerable<GetAllDetallesPagosVm>>> GetAllDetallesPagos()
        {
            try
            {
                var query = new GetAllDetallesPagosListQuery();
                var detallesPagos = await _mediator.Send(query);

                if (detallesPagos == null || !detallesPagos.Any())
                {
                    return NotFound("No se encontraron detalles pagos.");
                }

                return Ok(detallesPagos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetDetallePagoById
        [HttpGet("GetDetallePagoById/{id}")]
        public async Task<ActionResult<GetDetallePagoVm>> GetDetallePagoById(string id)
        {
            try
            {
                var command = new GetDetallePagoQuery(id);

                var detallePago = await _mediator.Send(command);

                if (detallePago == null)
                {
                    return NotFound();
                }

                return Ok(detallePago);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Detalle Pago con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateDetallePago
        [HttpPost("CreateDetallePago")]
        public async Task<ActionResult<string>> CreateDetallePago([FromBody] CreateDetallePagoCommand command)
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

        #region UpdateDetallePago
        [HttpPut("UpdateDetallePago")]
        public async Task<ActionResult> UpdateDetallePago([FromBody] UpdateDetallePagoCommand command)
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

        #region DeleteDetallePago
        [HttpDelete("DeleteDetallePago/{id}")]
        public async Task<ActionResult> DeleteDetallePago(string id)
        {
            try
            {
                var command = new DeleteDetallePagoCommand
                {
                    DetallePagoId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Detalle Pago con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
