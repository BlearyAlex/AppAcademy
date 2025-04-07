using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Ventas.Command.CreateVenta;
using AppAcademy.Application.Features.Ventas.Command.DeleteVenta;
using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IVentaRepository _ventaRepository;
        private readonly ILogger<VentaController> _logger;

        public VentaController(IMediator mediator, IVentaRepository ventaRepository, ILogger<VentaController> logger)
        {
            _mediator = mediator;
            _ventaRepository = ventaRepository;
            _logger = logger;
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

        #region CreateVenta
        [HttpPost("CreateVenta")]
        public async Task<ActionResult> CreateVenta([FromBody] CreateVentaCommand command)
        {
            try
            {
                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado");
                }

                command.UserName = userName;

                var result = await _mediator.Send(command);

                return Ok(new { message = "Venta creado con éxito", ventaId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }
        #endregion

        #region DeleteVenta
        [HttpDelete("DeleteVenta/{id}")]
        public async Task<ActionResult> DeleteVenta(string id)
        {
            try
            {
                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado.");
                }

                var command = new DeleteVentaCommand
                {
                    VentaId = id,
                    UserName = userName
                };

                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound($"Venta con ID {id} no encontrado.");
                }

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Venta con Id {id} no encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }
        #endregion

        #region SalesPerDay
        public async Task<IActionResult> SalesPerDay(DateTime startDate, DateTime endDate)
        {
            try
            {
                var salesPeroDay = await _ventaRepository.SalesPerDay(startDate, endDate);
                if (salesPeroDay == null || !salesPeroDay.Any())
                {
                    return NoContent();
                }

                return Ok(salesPeroDay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ventas por día: {Message}", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener las ventas por día.");
            }
        }
        #endregion
    }
}

