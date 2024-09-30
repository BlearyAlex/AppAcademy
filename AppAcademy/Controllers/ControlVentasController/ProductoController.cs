using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.DeleteProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        #region CreateProduct
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<string>> CreateProduct([FromBody] CreateProductoCommand command)
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

        #region UpdateProduct
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductoCommand command)
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
