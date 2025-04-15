using AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy;
using AppAcademy.Application.Features.AbonosAcademy.Commands.DeleteAbonoAcademy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Academia")]
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
                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    return Unauthorized("Usuario no autenticado");
                }

                command.UserName = userName;

                var result = await _mediator.Send(command);

                return File(result.PdfBlob, "application/pdf", $"Recibo-Abono-{result.AbonoAcademyId}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
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

                var command = new DeleteAbonoAcademyCommand
                {
                    AbonoAcademyId = id,
                    UserName = userName
                };

                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound($"Abono Academico con ID {id} no encontrado.");
                }

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
