using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.DeleteProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Application.Features.Productos.Queries.GetProductsMostSale;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlVentasController
{
    //[Authorize(Policy = "ManageProveedores")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductoController(IMediator mediator)
        {
            _mediator = mediator;
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

        #region GetProductsMostSale
        [HttpGet("GetProductsMostSale")]
        public async Task<IActionResult> GetProductsMostSale()
        {
            try
            {
                var products = await _mediator.Send(new GetProductsMostSaleQuery());
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
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                // Error relacionado con la carga de la imagen u otros procesos específicos
                return StatusCode(StatusCodes.Status400BadRequest, $"Error en la carga de la imagen: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");
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
                var command = new DeleteProductoCommand
                {
                    ProductoId = id
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.InnerException}");
            }
        }
        #endregion

        private bool IsValidImage(IFormFile image)
        {
            // Aquí podrías agregar validación del tipo de archivo y tamaño
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);

        }
    }
}
