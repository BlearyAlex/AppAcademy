using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.DeleteProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductosFilter;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Ventas")]
    public class ProductoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductoRepository _productoRepository;
        private readonly ILogger<ProductoController> _logger;
        private readonly IFileStorageService _fileStorageService;

        public ProductoController(IMediator mediator, IProductoRepository productoRepository, ILogger<ProductoController> logger, IFileStorageService fileStorageService)
        {
            _mediator = mediator;
            _productoRepository = productoRepository;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        #region GetAll
        [HttpGet("GetAllProductos")]
        public async Task<ActionResult<IEnumerable<GetAllProductosVm>>> GetAllProducts()
        {
            try
            {
                var query = new GetAllProductosListQuery();
                var products = await _mediator.Send(query);

                if (products == null || !products.Any())
                {
                    return NotFound("No se encontraron productos.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAllProductosFilter")]
        public async Task<ActionResult<IEnumerable<GetAllProductosVm>>> GetAllProductsFilter()
        {
            try
            {
                var query = new GetAllProductosFilterListQuery();
                var products = await _mediator.Send(query);

                if (products == null || !products.Any())
                {
                    return NotFound("No se encontraron productos.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetProductById
        [HttpGet("GetProductoById/{id}")]
        public async Task<ActionResult<GetProductByIdVm>> GetProductById(string id)
        {
            try
            {
                var command = new GetProductQuery(id);

                var product = await _mediator.Send(command);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
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

        #region GetProductsByCategoria
        [HttpGet("by-categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<GetProductsByCategoriaVm>>> GetProductsByCategoria(string categoria)
        {
            try
            {
                var query = new GetProductByCategoriaQuery(categoria);

                var products = await _mediator.Send(query);

                if (products == null || !products.Any())
                {

                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetProductsByName
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<GetProductsByNameVm>>> GetProductsByName(string name)
        {
            try
            {
                var query = new GetProductsByNameQuery(name);

                var products = await _mediator.Send(query);

                if (products == null || !products.Any())
                {

                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region CreateProduct
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<string>> CreateProduct([FromForm] CreateProductoCommand command)
        {
            try
            {
                // Validar si el archivo de la imagen es valido, si es necesario
                if (command.ImageFile != null && (command.ImageFile.Length == 0 || !IsValidImage(command.ImageFile)))
                {
                    return BadRequest("El archivo de la imagen no es válido.");
                }

                // Llamar al mediator para ejecutar el comando 
                var result = await _mediator.Send(command);

                // Devolver el Id del producto creado como respuesta
                return Ok(new { message = "Producto creado con éxito", productId = result });
            }
            catch (ApplicationException ex)
            {
                // Error relacionado con la carga de la imagen u otros procesos específicos
                return StatusCode(StatusCodes.Status400BadRequest, $"Error en la carga de la imagen: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor. ${ex.InnerException}");
            }
        }
        #endregion

        #region UpdateProduct
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductoCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            try
            {
                if (await _productoRepository.ProductoTieneVentasActivas(id))
                {
                    return Conflict(new { message = "No se puede eliminar el producto porque tiene ventas asociadas." });
                }

                var command = new DeleteProductoCommand
                {
                    ProductoId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Producto con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex}");
            }
        }
        #endregion

        #region DeleteImage
        [HttpDelete("DeleteImage/{*imageUrl}")]
        public async Task<IActionResult> DeleteImage(string imageUrl)
        {
            try
            {
                var imageName = Path.GetFileName(imageUrl);

                var result = await _fileStorageService.DeleteImage(imageUrl);

                if (!result)
                    return BadRequest("No se pudo eliminar la imagen.");

                var dbUpdate = await _productoRepository.CleanImageProductAsync(imageName);

                if (!dbUpdate)
                    return NotFound("Producto con esa imagen no encontrado.");

                return Ok(new { message = "Imagen eliminada correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la imagen}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
        #endregion

        #region GetSalesByProducto
        [HttpGet("sales-per-product")]
        public async Task<IActionResult> GetSalesByProduct(DateTime startDate, DateTime endDate)
        {
            try
            {
                var salesByProduct = await _productoRepository.GetSalesByProducto(startDate, endDate);
                if (salesByProduct == null || !salesByProduct.Any())
                {
                    return NoContent();
                }

                return Ok(salesByProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ventas por productos: {Message}", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener las ventas por productos.");
            }
        }
        #endregion

        #region GetSalesEvolutionByProduct
        [HttpGet("evolution-product")]
        public async Task<IActionResult> GetSalesEvolutionByProduct(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _productoRepository.GetSalesEvolutionByProducto(startDate, endDate);
                if (result == null || !result.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la evolucion por producto: {Message}", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener la evolucion por producto.");
            }
        }
        #endregion

        #region GetHighlightedProducts
        [HttpGet("highlighted-product")]
        public async Task<IActionResult> GetHighlightedProducts(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _productoRepository.GetHighlightedProducto(startDate, endDate);
                if (result == null || !result.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos destacados", ex.Message);
                return StatusCode(500, "Ocurrió un error al obtener los productos destacados.");
            }
        }
        #endregion

        #region IsValidImage
        private bool IsValidImage(IFormFile image)
        {
            // Aquí podrías agregar validación del tipo de archivo y tamaño
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);

        }
        #endregion
    }
}
