using AppAcademy.Application.Features.Entradas.Commands.CreateEntrada;
using AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada;
using AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada;
using AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas;
using AppAcademy.Application.Features.Entradas.Queries.GetEntrada;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using AppAcademy.Application.Features.Entradas.Queries.GetEntradasForMonth;
using Microsoft.AspNetCore.Authorization;


namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Ventas")]
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

        #region GetEntradaById
        [HttpGet("GetEntradaById/{id}")]
        public async Task<ActionResult<GetEntradaVm>> GetEntradaById(string id)
        {
           
                var command = new GetEntradaQuery(id);

                var entrada = await _mediator.Send(command);

                return Ok(entrada);
        }
        #endregion

        #region GetEntradaForMonth
        [HttpGet("GetEntradaForMont")]
        public async Task<ActionResult<GetEntradasForMonthVm>> GetEntradForMont()
        {
            try
            {
                var command = new GetEntradasForMonthQuery();

                var entrada = await _mediator.Send(command);

                return Ok(entrada);
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
            var result = await _mediator.Send(command);
            return Ok(result);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region DeleteEntrada
        [HttpDelete("DeleteEntrada/{id}")]
        public async Task<ActionResult> DeleteEntrada(string id)
        {
           await _mediator.Send(new DeleteEntradaCommand(id));
            return NoContent();
        }
        #endregion
    }
}
