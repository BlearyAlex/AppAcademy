using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Marcas.Command.CreateMarca;
using AppAcademy.Application.Features.Marcas.Command.DeleteMarca;
using AppAcademy.Application.Features.Marcas.Command.UpdateMarca;
using AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas;
using AppAcademy.Application.Features.Marcas.Queries.GetMarca;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MarcaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllMarcas")]
        public async Task<ActionResult<IEnumerable<GetAllMarcasVm>>> GetAllMarcas()
        {
            try
            {
                var query = new GetAllMarcasListQuery();
                var marcas = await _mediator.Send(query);

                if(marcas == null || !marcas.Any())
                {
                    return NotFound("No se encontraron las marcas.");
                }

                return Ok(marcas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetMarcaById
        [HttpGet("GetMarcaById/{id}")]
        public async Task<ActionResult<GetMarcaVm>> GetMarcaById(string id)
        {
            try
            {
                var command = new GetMarcaQuery(id);
                var marca = await _mediator.Send(command);

                if(marca == null)
                {
                    return NotFound();
                }

                return Ok(marca);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateMarca
        [HttpPost("CreateMarca")]
        public async Task<ActionResult<string>> CreateMarca([FromBody] CreateMarcaCommand command)
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

        #region UpdateMarca
        [HttpPut("UpdateCategoria")]
        public async Task<ActionResult> UpdateMarca([FromBody] UpdateMarcaCommand command)
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

        #region DeleteMarca
        [HttpDelete("DeleteMarca/{id}")]
        public async Task<ActionResult> DeleteMarca(string id)
        {
            try
            {
                var command = new DeleteMarcaCommand
                {
                    MarcaId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Marca con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }

}

