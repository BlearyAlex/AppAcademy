using AppAcademy.Application.Features.Estudiantes.Commands.CreateEstudiante;
using AppAcademy.Application.Features.Estudiantes.Commands.DeleteEstudiante;
using AppAcademy.Application.Features.Estudiantes.Commands.UpdateEstudiante;
using AppAcademy.Application.Features.Estudiantes.Queries.GetAllEstudiantes;
using AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteById;
using AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteWithColegiaturas;
using MediatR;
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

                if (students == null)
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

        #region GetEstudianteWithColegiatura
        [HttpGet("GetEstudianteWithColegiatura/{estudianteId}")]
        public async Task<ActionResult<GetEstudianteWithColegiaturaVm>> GetEstudianteWithColegiatura(string estudianteId)
        {
            try
            {
                var command = new GetEstudianteWithColegiaturaQuery(estudianteId);

                var student = await _mediator.Send(command);

                if (student == null)
                {
                    return NoContent();
                }

                return Ok(student);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region CreateEstudiante
        [HttpPost("CreateEstudiante")]
        public async Task<ActionResult<string>> CreateEstudiante([FromForm] CreateEstudianteCommand command)
        {
            try
            {
                if (command.ImageFile != null && (command.ImageFile.Length == 0 || !IsValidImage(command.ImageFile)))
                {
                    return BadRequest("El archivo de la imagen no es válido.");
                }

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region UpdateEstudiante
        [HttpPut("UpdateEstudiante")]
        public async Task<ActionResult> UpdateEstudiante([FromForm] UpdateEstudianteCommand command)
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
     private bool IsValidImage(IFormFile image)
        {
            // Aquí podrías agregar validación del tipo de archivo y tamaño
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);

        }
    }

}
