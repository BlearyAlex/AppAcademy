using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.AcademicCycles.Commands.CreateCycle;
using AppAcademy.Application.Features.AcademicCycles.Commands.DeleteCycle;
using AppAcademy.Application.Features.AcademicCycles.Commands.UpdateCycle;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetMonthsAvailables;
using AppAcademy.Application.Features.Careers.Commands.CreateCareer;
using AppAcademy.Application.Features.Careers.Commands.DeleteCareer;
using AppAcademy.Application.Features.Careers.Commands.UpdateCareer;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareers;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using AppAcademy.Infrastucture.Repositories.ControlAcademia;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AcademicCycleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAcademicCycleRepository _academicCycleRepository;

        public AcademicCycleController(IMediator mediator, IAcademicCycleRepository academicCycleRepository)
        {
            _mediator = mediator;
            _academicCycleRepository = academicCycleRepository;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateCycleCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(new { message = "Ciclo Academico creado exitosamente.", academicCycleId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateCycleCommand command)
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

        #region Delete
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _academicCycleRepository.AcademicCycleTieneEstudiantes(id))
                {
                    return Conflict(new { message = "No se puede eliminar el ciclo academico porque tiene estudiantes asociados." });
                }

                var command = new DeleteCycleCommand
                {
                    AcademicCycleId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Ciclo Academico con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllCyclesVm>>> GetAll()
        {
            try
            {
                var query = new GetAllCyclesListQuery();
                var cycles = await _mediator.Send(query);

                if (cycles == null || !cycles.Any())
                {
                    return NotFound("No se encontraron ciclos academicos.");
                }

                return Ok(cycles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetCycleVm>> GetById(int id)
        {
            try
            {
                var command = new GetCycleQuery(id);

                var career = await _mediator.Send(command);

                if (career == null)
                {
                    return NotFound();
                }

                return Ok(career);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Ciclo Academico con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        [HttpGet("available-months/{studentId}/{academicCycleId}")]
        public async Task<IActionResult> GetAvailableMonths(int studentId, int academicCycleId)
        {
            try
            {
                var query = new GetAvailableMonthsQuery(studentId, academicCycleId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Podrías personalizar la respuesta dependiendo del tipo de excepción
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
