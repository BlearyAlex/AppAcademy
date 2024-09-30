using AppAcademy.Application.Features.Clientes.Commands.CreateCliente;
using AppAcademy.Application.Features.Clientes.Commands.DeleteCliente;
using AppAcademy.Application.Features.Clientes.Commands.UpdateCliente;
using AppAcademy.Application.Features.Clientes.Queries.GetAllCliente;
using AppAcademy.Application.Features.Clientes.Queries.GetClienteById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<GetAllClientesVm>>> GetAllClientes()
        {
            try
            {
                var query = new GetAllClientesListQuery();
                var clientes = await _mediator.Send(query);

                if (clientes == null || !clientes.Any())
                {
                    return NotFound("No se encontraron clientes.");
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetClientById
        [HttpGet("GetClientById/{id}")]
        public async Task<ActionResult<GetClienteVm>> GetClientById(string id)
        {
            try
            {
                var command = new GetClienteQuery(id);

                var cliente = await _mediator.Send(command);

                if (cliente == null)
                {
                    return NotFound();
                }

                return Ok(cliente);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Cliente con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateClient
        [HttpPost("CreateClient")]
        public async Task<ActionResult<string>> CreateClient([FromBody] CreateClienteCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region UpdateCliente
        [HttpPut("UpdateClient")]
        public async Task<ActionResult> UpdateClient([FromBody] UpdateClienteCommand command)
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

        #region DeleteClient
        [HttpDelete("DeleteClient/{id}")]
        public async Task<ActionResult> DeleteClient(string id)
        {
            try
            {
                var command = new DeleteClienteCommand
                {
                    ClienteId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Cliente con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
