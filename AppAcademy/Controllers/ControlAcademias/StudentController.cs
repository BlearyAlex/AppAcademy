using AppAcademy.Application.Exceptions;
using AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using AppAcademy.Application.Features.Students.Commands.CreateStudent;
using AppAcademy.Application.Features.Students.Commands.DeleteStudent;
using AppAcademy.Application.Features.Students.Commands.UpdateStudent;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetGanttData;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
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

        private bool IsValidImage(IFormFile image)
        {
            // Aquí podrías agregar validación del tipo de archivo y tamaño
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);

        }
    }
}
