using AppAcademy.Application.Features.Entradas.Commands.CreateEntrada;
using AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada;
using AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada;
using AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas;
using AppAcademy.Application.Features.Entradas.Queries.GetEntrada;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;


namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EntradaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntradaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllEntradas")]
        public async Task<ActionResult<IEnumerable<GetAllEntradasVm>>> GetAllEntradas()
        {
            try
            {
                var query = new GetAllEntradasListQuery();
                var entradas = await _mediator.Send(query);

                if (entradas == null || !entradas.Any())
                {
                    return NotFound("No se encontraron entradas.");
                }

                return Ok(entradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetEntradaById
        [HttpGet("GetEntradaById/{id}")]
        public async Task<ActionResult<GetEntradaVm>> GetEntradaById(string id)
        {
            try
            {
                var command = new GetEntradaQuery(id);

                var entrada = await _mediator.Send(command);

                if (entrada == null)
                {
                    return NotFound();
                }

                return Ok(entrada);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Entrada con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateEntrada
        [HttpPost("CreateEntrada")]
        public async Task<ActionResult<string>> CreateEntrada([FromBody] CreateEntradaCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    Message = "Se presentaron errores de validación.",
                    Errors = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region UpdateEntrada
        [HttpPut("UpdateEntrada")]
        public async Task<ActionResult> UpdateEntrada([FromBody] UpdateEntradaCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region DeleteEntrada
        [HttpDelete("DeleteEntrada/{id}")]
        public async Task<ActionResult> DeleteEntrada(string id)
        {
            try
            {
                var command = new DeleteEntradaCommand
                {
                    EntradaId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Entrada con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
