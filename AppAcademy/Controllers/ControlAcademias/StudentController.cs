using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Application.Features.Students.Commands.CreateStudent;
using AppAcademy.Application.Features.Students.Commands.DeleteStudent;
using AppAcademy.Application.Features.Students.Commands.UpdateStudent;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetGanttData;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStudentRepository _studentRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IMediator mediator, IStudentRepository studentRepository, IFileStorageService fileStorageService, ILogger<StudentController> logger)
        {
            _mediator = mediator;
            _studentRepository = studentRepository;
            _fileStorageService = fileStorageService;
            _logger = logger;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromForm] CreateStudentCommand command)
        {
            try
            {
                if (command.ImageFile != null && (command.ImageFile.Length == 0 || !IsValidImage(command.ImageFile)))
                {
                    return BadRequest("El archivo de la imagen no es valido.");
                }

                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado");
                }

                command.UserName = userName;

                var result = await _mediator.Send(command);

                return Ok(new { message = "Student creado con éxito", studentId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromForm] UpdateStudentCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estudiante");
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
                if (await _studentRepository.StudentTienePagosActivos(id))
                {
                    return Conflict(new { message = "No se puede eliminar el estudiante porque tiene pagos asociados." });
                }

                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado.");
                }

                var command = new DeleteStudentCommand
                {
                    StudentId = id,
                    UserName = userName
                };

                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound($"Estudiante con ID {id} no encontrado.");
                }

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllStudentsVm>>> GetAll()
        {
            try
            {
                var query = new GetAllStudentsListQuery();
                var students = await _mediator.Send(query);

                if (students == null || !students.Any())
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

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetStudentVm>> GetById(int id)
        {
            try
            {
                var command = new GetStudentQuery(id);

                var categoria = await _mediator.Send(command);

                if (categoria == null)
                {
                    return NotFound();
                }

                return Ok(categoria);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Categoría con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetStudentsWithMonths
        [HttpGet("GetStudentsWithMonths")]
        public async Task<ActionResult<GetStudentCardVm>> GetStudentsWithMonthsGetGantt()
        {
            try
            {
                var command = new GetGanttDataListQuery();

                var gantt = await _mediator.Send(command);

                if (gantt == null)
                {
                    return NotFound();
                }

                return Ok(gantt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region DeleteImage
        [HttpDelete("DeleteImage/{*imageUrl}")]
        public async Task<IActionResult> DeleteImage(string imageUrl)
        {
            try
            {
                var imageName = Path.GetFileName(imageUrl);

                var result = await _fileStorageService.DeleteImage(imageUrl);

                if (!result)
                    return BadRequest("No se pudo eliminar la imagen.");

                var dbUpdate = await _studentRepository.CleanImageStudentAsync(imageName);

                if (!dbUpdate)
                    return NotFound("Student con esa imagen no encontrado.");

                return Ok(new { message = "Imagen eliminada correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la imagen}");
                return StatusCode(500, $"Error interno: {ex.Message}");
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
