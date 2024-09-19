using AppAcademy.Application.Features.Clientes.Queries.GetClienteById;
using AppAcademy.Application.Features.Cortes.Commands.CreateCorte;
using AppAcademy.Application.Features.Cortes.Commands.DeleteCorte;
using AppAcademy.Application.Features.Cortes.Commands.UpdateCorte;
using AppAcademy.Application.Features.Cortes.Queries.GetAllCortes;
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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CorteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CorteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllCortes")]
        public async Task<ActionResult<IEnumerable<GetAllDetallesCortesVm>>> GetAllCortes()
        {
            try
            {
                var query = new GetAllCortesListQuery();
                var cortes = await _mediator.Send(query);

                if (cortes == null || !cortes.Any())
                {
                    return NotFound("No se encontraron cortes.");
                }

                return Ok(cortes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetCorteById
        [HttpGet("GetCorteById/{id}")]
        public async Task<ActionResult<GetCorteVm>> GetCorteById(string id)
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
                return NotFound($"Corte con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateCorte
        [HttpPost("CreateCorte")]
        public async Task<ActionResult<string>> CreateCorte([FromBody] CreateCorteCommand command)
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

        #region UpdateCorte
        [HttpPut("UpdateCorte")]
        public async Task<ActionResult> UpdateCorte([FromBody] UpdateCorteCommand command)
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

        #region DeleteCorte
        [HttpDelete("DeleteCorte/{id}")]
        public async Task<ActionResult> DeleteCorte(string id)
        {
            try
            {
                var command = new DeleteCorteCommand
                {
                    CorteId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Corte con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
