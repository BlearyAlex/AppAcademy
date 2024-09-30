using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAll
        [HttpGet("GetAllCategorias")]
        public async Task<ActionResult<IEnumerable<GetAllCategoriasVm>>> GetAllCategorias()
        {
            try
            {
                var query = new GetAllCategoriasListQuery();
                var categorias = await _mediator.Send(query);

                if (categorias == null || !categorias.Any())
                {
                    return NotFound("No se encontraron categorías.");
                }

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetCategoriaById
        [HttpGet("GetCategoriaById/{id}")]
        public async Task<ActionResult<GetCategoriaByIdVm>> GetCategoriaById(string id)
        {
            try
            {
                var command = new GetCategoriaByIdQuery(id);

                var categoria = await _mediator.Send(command);

                if (categoria == null)
                {
                    return NotFound();
                }

                return Ok(categoria);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Categoría con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateCategoria
        [HttpPost("CreateCategoria")]
        public async Task<ActionResult<string>> CreateCategoria([FromBody] CreateCategoriaCommand command)
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

        #region UpdateCategoria
        [HttpPut("UpdateCategoria")]
        public async Task<ActionResult> UpdateCategoria([FromBody] UpdateCategoriaCommand command)
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

        #region DeleteCategoria
        [HttpDelete("DeleteCategoria/{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            try
            {
                var command = new DeleteCategoriaCommand
                {
                    CategoriaId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Categoría con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
