using AppAcademy.Application.Features.StudentPaymentStatuses.Commands.CreateStudentPaymentStatus;
using AppAcademy.Application.Features.StudentPaymentStatuses.Commands.DeleteStudentPaymentStatus;
using AppAcademy.Application.Features.StudentPaymentStatuses.Commands.UpdateStudentPaymentStatus;
using AppAcademy.Application.Features.StudentPaymentStatuses.Queries.GetAllStudentPaymentStatuses;
using AppAcademy.Application.Features.StudentPaymentStatuses.Queries.GetStudentPaymentStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Academia")]
    public class StudentPaymentStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentPaymentStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateStudentPaymentStatusComand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(new { message = "Creado exitosamente.", careerId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateStudentPaymentStatusCommand command)
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
                var command = new DeleteStudentPaymentStatusCommand
                {
                    StudentPaymentStatusId = id
                };

                await _mediator.Send(command);

                return NoContent();
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

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllStudentPaymentStatusesVm>>> GetAll()
        {
            try
            {
                var query = new GetAllStudentPaymentStatusesQuery();
                var studentPayment = await _mediator.Send(query);

                if (studentPayment == null || !studentPayment.Any())
                {
                    return NotFound("No se encontraron.");
                }

                return Ok(studentPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetStudentPaymentStatusVm>> GetById(int id)
        {
            try
            {
                var command = new GetStudentPaymentStatusQuery(id);

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
