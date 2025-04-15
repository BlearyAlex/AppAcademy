using AppAcademy.Application.Features.Abonos.Command.CreateAbono;
using AppAcademy.Application.Features.Abonos.Command.DeleteAbono;
using AppAcademy.Application.Features.Ventas.Command.DeleteVenta;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Ventas")]
    public class AbonoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AbonoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AbonarVenta([FromBody] CreateAbonoCommand command)
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

                return File(result.PdfBlob, "application/pdf", $"Recibo-Abono-{result.AbonoId}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAbono(int id)
        {
            try
            {
                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado.");
                }

                var command = new DeleteAbonoCommand
                {
                    AbonoId = id,
                    UserName = userName
                };

                var result = await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Abono con Id {id} no encontrada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }
    }
}
