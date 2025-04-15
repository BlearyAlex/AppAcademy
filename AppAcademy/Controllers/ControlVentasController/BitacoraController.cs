using AppAcademy.Application.Features.Bitacora.Queries.GetBitacoras;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BitacoraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BitacoraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetBitacorasVm>>> GetAllBitacoras()
        {
            try
            {
                var query = new GetBitacorasListQuery();
                var bitacoras = await _mediator.Send(query);

                if (bitacoras == null || !bitacoras.Any())
                {
                    return NoContent();
                }

                return Ok(bitacoras);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }
    }
}
