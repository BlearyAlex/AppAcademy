using AppAcademy.Application.Features.Abonos.Command.CreateAbono;
using AppAcademy.Application.Features.Abonos.Command.DeleteAbono;
using AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy;
using AppAcademy.Application.Features.AbonosAcademy.Commands.DeleteAbonoAcademy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AbonoAcademyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AbonoAcademyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AbonarPago([FromBody] CreateAbonoAcademyCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { message = "Abono creado con éxito", paymentId = result });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAbono(int id)
        {
            try
            {
                var command = new DeleteAbonoAcademyCommand
                {
                    AbonoAcademyId = id
                };

                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Abono con Id {id} no encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
