using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using AppAcademy.Infrastucture.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(IMediator mediator, ICategoriaRepository categoriaRepository, ILogger<CategoriaController> logger)
        {
            _mediator = mediator;
            _categoriaRepository = categoriaRepository;
            _logger = logger;
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
                var result =  await _mediator.Send(command);

                return Ok(new { message = "Categoria creada exitosamente.", categoriaId = result });
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
                if (await _categoriaRepository.CategoriaTieneProductosActivos(id))
                {
                    return Conflict(new { message = "No se puede eliminar la categoria porque tiene productos asociados." });
                }

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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }
        #endregion

        #region SalesByCategory
        [HttpGet("sales-per-category")]
        public async Task<IActionResult> SalesByCategory(DateTime startDate, DateTime endDate)
        {
            try
            {
                var salesByCategory = await _categoriaRepository.GetSalesByCategory(startDate, endDate);
                if (salesByCategory == null || !salesByCategory.Any())
                {
                    return NoContent();
                }

                return Ok(salesByCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ventas por categoria: {Message}", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener las ventas por categoria.");
            }
        }
        #endregion

        #region GetSalesEvolutionByCategory
        [HttpGet("evolution-category")]
        public async Task<IActionResult> GetSalesEvolutionByCategory(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _categoriaRepository.GetSalesEvolutionByCategory(startDate, endDate);
                if (result == null || !result.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la evolucion por categoria: {Message}", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener la evolucion por categoria.");
            }
        }
        #endregion

        #region GetHighlightedCategories
        [HttpGet("highlighted-category")]
        public async Task<IActionResult> GetHighlightedCategories(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _categoriaRepository.GetHighlightedCategories(startDate, endDate);
                if (result == null || !result.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las categorias destacadas", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener las categorias destacadas.");
            }
        }
        #endregion
    }
}
