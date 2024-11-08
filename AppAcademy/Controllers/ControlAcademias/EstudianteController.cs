using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using AppAcademy.Application.Features.Estudiantes.Commands.CreateEstudiante;
using AppAcademy.Application.Features.Estudiantes.Commands.DeleteEstudiante;
using AppAcademy.Application.Features.Estudiantes.Commands.UpdateEstudiante;
using AppAcademy.Application.Features.Estudiantes.Queries.GetAllEstudiantes;
using AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstudianteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllEstudiantes")]
        public async Task<ActionResult<IEnumerable<GetAllEstudiantesVm>>> GetAllEstudiantes()
        {
            try
            {
                var query = new GetAllEstudiantesListQuery();
                var students = await _mediator.Send(query);

                if(students == null)
                {
                    return NotFound("No se encontraron estudiantes.");
                }

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetEstudianteById
        [HttpGet("GetEstudianteById/{id}")]
        public async Task<ActionResult<GetEstudianteByIdVm>> GetEstudianteById(string id)
        {
            try
            {
                var command = new GetEstudianteByIdQuery(id);

                var student = await _mediator.Send(command);

                if (student == null)
                {
                    return NoContent();
                }

                return Ok(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Estudiante con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateEstudiante
        [HttpPost("CreateEstudiante")]
        public async Task<ActionResult<string>> CreateEstudiante([FromBody] CreateEstudianteCommand command)
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

        #region UpdateEstudiante
        [HttpPut("UpdateEstudiante")]
        public async Task<ActionResult> UpdateEstudiante([FromBody] UpdateEstudianteCommand command)
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

        #region DeleteEstudiante
        [HttpDelete("DeleteEstudiante/{id}")]
        public async Task<ActionResult> DeleteEstudiante(string id)
        {
            try
            {
                var command = new DeleteEstudianteCommand
                {
                    EstudianteId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Estudiante con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
