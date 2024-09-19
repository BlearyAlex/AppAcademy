using AppAcademy.Application.Features.Clientes.Queries.GetClienteById;
using AppAcademy.Application.Features.Cortes.Queries.GetCorte;
using AppAcademy.Application.Features.DetallesCortes.Command.CreateDetalleCorte;
using AppAcademy.Application.Features.DetallesCortes.Command.DeleteDetalleCorte;
using AppAcademy.Application.Features.DetallesCortes.Command.UpdateDetalleCorte;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetDetalleCorte;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCorteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DetalleCorteController(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region GetAll
        [HttpGet("GetAllDetallesCortes")]
        public async Task<ActionResult<IEnumerable<GetAllDetallesCortesVm>>> GetAllDetallesCortes()
        {
            try
            {
                var query = new GetAllDetallesCortesListQuery();
                var detallesCortes = await _mediator.Send(query);

                if (detallesCortes == null || !detallesCortes.Any())
                {
                    return NotFound("No se encontraron detalles cortes.");
                }

                return Ok(detallesCortes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetDetalleCorteById
        [HttpGet("GetDetalleCorteById/{id}")]
        public async Task<ActionResult<GetDetalleCorteVm>> GetDetalleCorteById(string id)
        {
            try
            {
                var command = new GetCorteQuery(id);

                var detalleCorte = await _mediator.Send(command);

                if (detalleCorte == null)
                {
                    return NotFound();
                }

                return Ok(detalleCorte);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Detalle Corte con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateDetalleCorte
        [HttpPost("CreateDetalleCorte")]
        public async Task<ActionResult<string>> CreateDetalleCorte([FromBody] CreateDetalleCorteCommand command)
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

        #region UpdateDetalleCorte
        [HttpPut("UpdateDetalleCorte")]
        public async Task<ActionResult> UpdateDetalleCorte([FromBody] UpdateDetalleCorteCommand command)
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

        #region DeleteDetalleCorte
        [HttpDelete("DeleteDetalleCorte/{id}")]
        public async Task<ActionResult> DeleteDetalleCorte(string id)
        {
            try
            {
                var command = new DeleteDetalleCorteCommand
                {
                    DetalleCorteId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Detalle Corte con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
