using AppAcademy.Application.Features.Payments.Commands.CreatePayment;
using AppAcademy.Application.Features.Payments.Commands.DeletePayment;
using AppAcademy.Application.Features.Payments.Commands.UpdatePayment;
using AppAcademy.Application.Features.Payments.Queries.GetAllPayments;
using AppAcademy.Application.Features.Payments.Queries.GetPayment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
                var result = await _mediator.Send(command);

                return Ok(new { message = "Pago creado exitosamente.", paymentId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdatePaymentCommand command)
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
                var command = new DeletePaymentCommand
                {
                    PaymentId = id
                };

                await _mediator.Send(command);

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

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllPaymentsVm>>> GetAll()
        {
            try
            {
                var query = new GetAllPaymentListQuery();
                var payments = await _mediator.Send(query);

                if (payments == null || !payments.Any())
                {
                    return NotFound("No se encontraron pagos.");
                }

                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetPaymentVm>> GetById(int id)
        {
            try
            {
                var command = new GetPaymentQuery(id);

                var payment = await _mediator.Send(command);

                if (payment == null)
                {
                    return NotFound();
                }

                return Ok(payment);
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
