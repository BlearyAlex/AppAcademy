using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.DeleteProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppAcademy.Controllers
{
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
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<GetAllProductosVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GetAllProductosVm>>> GetAllProducts()
        {
            var query = new GetAllProductosListQuery();

            var products = await _mediator.Send(query);
            return Ok(products);
        }
        #endregion

        #region GetProductById
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetProductByIdVm>> GetProductById(string id)
        {
            var command = new GetProductQuery(id);

            var product = await _mediator.Send(command);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        #endregion

        [HttpGet("by-categoria/{categoria}", Name = "GetByCategoria")]
        [ProducesResponseType(typeof(IEnumerable<GetProductsByCategoriaVm>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetProductsByCategoriaVm>>> GetProductsByCategoria(string categoria)
        {
            var query = new GetProductByCategoriaQuery(categoria);

            var products = await _mediator.Send(query);

            // Verifica si se encontraron productos
            if (products == null || !products.Any())
            {
                // Retorna 404 Not Found si no se encuentran productos
                return NotFound();
            }

            return Ok(products);
        }

        #region CreateProduct
        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType( (int)HttpStatusCode.Created)]
        public async Task<ActionResult<string>> CreateProduct([FromBody] CreateProductoCommand command)
        {
           return await _mediator.Send(command);
        }
        #endregion

        #region UpdateProduct
        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductoCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductoCommand
            {
                ProductoId = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
        #endregion
    }
}
