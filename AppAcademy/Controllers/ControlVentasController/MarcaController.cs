using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Marcas.Command.CreateMarca;
using AppAcademy.Application.Features.Marcas.Command.DeleteMarca;
using AppAcademy.Application.Features.Marcas.Command.UpdateMarca;
using AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas;
using AppAcademy.Application.Features.Marcas.Queries.GetMarca;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMarcaRepository _marcaRepository;

        public MarcaController(IMediator mediator, IMarcaRepository marcaRepository)
        {
            _mediator = mediator;
            _marcaRepository = marcaRepository;
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
                var result = await _mediator.Send(command);

                return Ok(new { message = "Marca creada exitosamente.", marcaId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region UpdateMarca
        [HttpPut("UpdateMarca")]
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
                if (await _marcaRepository.MarcaTieneProductosActivos(id))
                {
                    return Conflict(new { message = "No se puede eliminar la marca porque tiene productos asociados." });
                }

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

