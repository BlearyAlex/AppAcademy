using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Productos.Commands.DeleteProducto
{
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductoCommandHandler> _logger;

        public DeleteProductoCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogger<DeleteProductoCommandHandler> logger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            var deleteProducto = await _productoRepository.GetById(request.ProductoId);
            if (deleteProducto == null)
            {
                _logger.LogError($"{request.ProductoId} producto no existe en el sistma");
                throw new NotFoundException(nameof(deleteProducto), request.ProductoId);
            }

            await _productoRepository.DeleteAsync(deleteProducto);
            _logger.LogInformation($"El {request.ProductoId} fue eliminado con exito");

            return;
        }
    }
}
