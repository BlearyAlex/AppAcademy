using AppAcademy.Application.Features.Colegiaturas.Commands.CreateColegiatura;
using AppAcademy.Application.Features.Colegiaturas.Commands.DeleteColegiatura;
using AppAcademy.Application.Features.Colegiaturas.Commands.UpdateColegiatura;
using AppAcademy.Application.Features.Colegiaturas.Queries.GetAllColegiaturas;
using AppAcademy.Application.Features.Colegiaturas.Queries.GetColegiatura;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColegiaturaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ColegiaturaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllColegiaturas")]
        public async Task<ActionResult<IEnumerable<GetAllColegiaturasVm>>> GetAllColegiaturas()
        {
            try
            {
                var query = new GetAllColegiaturasListQuery();
                var colegiaturas = await _mediator.Send(query);

                if (colegiaturas == null)
                {
                    return NotFound("No se encontraron colegiaturas.");
                }

                return Ok(colegiaturas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetColegiaturaById
        [HttpGet("GetColegiaturaById/{id}")]
        public async Task<ActionResult<GetColegiaturaVm>> GetColegiaturaById(string id)
        {
            try
            {
                var command = new GetColegiaturaQuery(id);

                var colegiatura = await _mediator.Send(command);

                if (colegiatura == null)
                {
                    return NoContent();
                }

                return Ok(colegiatura);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Colegiatura con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateColegiatura
        [HttpPost("CreateColegiatura")]
        public async Task<ActionResult<string>> CreateColegiatura([FromBody] CreateColegiaturaCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region UpdateColegiatura
        [HttpPut("UpdateColegiatura")]
        public async Task<ActionResult> UpdateColegiatura([FromBody] UpdateColegiaturaCommand command)
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

        #region DeleteColegiatura
        [HttpDelete("DeleteColegiatura/{id}")]
        public async Task<ActionResult> DeleteColegiatura(string id)
        {
            try
            {
                var command = new DeleteColegiaturaCommand
                {
                    ColegiaturaId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Colegiatura con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
