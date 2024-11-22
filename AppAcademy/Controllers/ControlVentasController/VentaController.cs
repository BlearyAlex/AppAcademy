using AppAcademy.Application.Features.Entradas.Commands.CreateEntrada;
using AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada;
using AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada;
using AppAcademy.Application.Features.Entradas.Queries.GetEntrada;
using AppAcademy.Application.Features.Ventas.Command.CreateVenta;
using AppAcademy.Application.Features.Ventas.Command.DeleteVenta;
using AppAcademy.Application.Features.Ventas.Command.UpdateVenta;
using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
using AppAcademy.Application.Features.Ventas.Queries.GetVentasForDate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAllVentas
        [HttpGet("GetAllVentas")]
        public async Task<ActionResult<IEnumerable<GetAllVentasVm>>> GetAllVentas()
        {
            try 
            {
                var query = new GetAllVentasListQuery();
                var entradas = await _mediator.Send(query);

                if (entradas == null || !entradas.Any())
                {
                    return NoContent();
                }

                return Ok(entradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetVentaById
        [HttpGet("GetVentaById/{id}")]
        public async Task<ActionResult<GetVentaVm>> GetVentaById(string id)
        {

            var command = new GetVentaQuery(id);

            var venta = await _mediator.Send(command);

            return Ok(venta);
        }
        #endregion

        #region GetVentasForDate
        [HttpGet("GetVentasForDate")]
        public async Task<IActionResult> GetVentasForDate([FromQuery] string periodo)
        {
            try
            {
                if (string.IsNullOrEmpty(periodo))
                {
                    return BadRequest("Debe especificar el periodo: 'dia', 'semana' o 'mes'.");
                }

                var result = await _mediator.Send(new GetVentasForDateQuery(periodo));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateVenta
        [HttpPost("CreateVenta")]
        public async Task<ActionResult<string>> CreateVenta([FromBody] CreateVentaCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region UpdateVenta
        [HttpPut("UpdateVenta")]
        public async Task<ActionResult> UpdateVenta([FromBody] UpdateVentaCommand command)
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

        #region DeleteVenta
        [HttpDelete("DeleteVenta/{id}")]
        public async Task<ActionResult> DeleteVenta(string id)
        {
            try
            {
                var command = new DeleteVentaCommand
                {
                    VentaId = id
                };

                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Venta con Id {id} no encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}

