using AppAcademy.Application.Features.Abonos.Command.CreateAbono;
using AppAcademy.Application.Features.Abonos.Command.DeleteAbono;
using AppAcademy.Application.Features.Ventas.Command.DeleteVenta;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AbonoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("abonar")]
        public async Task<IActionResult> AbonarVenta([FromBody] CreateAbonoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAbono(int id)
        {
            try
            {
                var command = new DeleteAbonoCommand
                {
                    AbonoId = id
                };

                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Venta con Id {id} no encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
