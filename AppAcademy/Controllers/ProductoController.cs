using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.DeleteProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
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

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<GetAllProductosVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GetAllProductosVm>>> GetAllProducts()
        {
            var query = new GetAllProductosListQuery();

            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType( (int)HttpStatusCode.Created)]
        public async Task<ActionResult<string>> CreateProduct([FromBody] CreateProductoCommand command)
        {
           return await _mediator.Send(command);
        }

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductoCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{string}", Name = "DeleteProduct")]
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
    }
}
