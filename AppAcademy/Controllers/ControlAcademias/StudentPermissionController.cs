using AppAcademy.Application.Features.Careers.Commands.CreateCareer;
using AppAcademy.Application.Features.Careers.Commands.DeleteCareer;
using AppAcademy.Application.Features.Careers.Commands.UpdateCareer;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareers;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using AppAcademy.Application.Features.StudentPermissions.Commands.CreateStudentPermission;
using AppAcademy.Application.Features.StudentPermissions.Commands.DeleteStudentPermission;
using AppAcademy.Application.Features.StudentPermissions.Commands.UpdateStudentPermission;
using AppAcademy.Application.Features.StudentPermissions.Queries.GetAllStudentPermissions;
using AppAcademy.Application.Features.StudentPermissions.Queries.GetStudentPermission;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1[controller]")]
    [ApiController]
    public class StudentPermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentPermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateStudentPermissionCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(new { message = "Student Permission creada exitosamente.", careerId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateStudentPermissionCommand command)
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
                var command = new DeleteStudentPermissionCommand
                {
                    StudentPermissionId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Student Permission con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllStudentPermissionsVm>>> GetAll()
        {
            try
            {
                var query = new GetAllStudentPermissionsListQuery();
                var studentPayments = await _mediator.Send(query);

                if (studentPayments == null || !studentPayments.Any())
                {
                    return NotFound("No se encontraron students payments.");
                }

                return Ok(studentPayments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetStudentPermissionVm>> GetById(int id)
        {
            try
            {
                var command = new GetStudentPermissionQuery(id);

                var studentPayment = await _mediator.Send(command);

                if (studentPayment == null)
                {
                    return NotFound();
                }

                return Ok(studentPayment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Student Payment con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
