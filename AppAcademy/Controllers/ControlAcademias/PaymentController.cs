using AppAcademy.Application.Features.Payments.Commands.CreatePayment;
using AppAcademy.Application.Features.Payments.Commands.DeletePayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Academia")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreatePaymentCommand command)
        {
            try
            {
                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado");
                }

                command.UserName = userName;

                var result = await _mediator.Send(command);

                return Ok(new { message = "Pago creado exitosamente.", paymentId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetPayments
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetPaymentsVm>>> GetAll()
        {
            try
            {
                var query = new GetPaymentsListQuery();
                var payments = await _mediator.Send(query);

                if (payments == null || !payments.Any())
                {
                    return NoContent();
                }

                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetPayment
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetPaymentVm>> GetById(int id)
        {
            try
            {
                var query = new GetPaymentQuery(id);

                var payment = await _mediator.Send(query);

                if (payment == null)
                {
                    return NoContent();
                }

                return Ok(payment);
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

                var command = new DeletePaymentCommand
                {
                    PaymentId = id,
                    UserName = userName
                };

                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound($"Estudiante con ID {id} no encontrado.");
                }

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pago con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
