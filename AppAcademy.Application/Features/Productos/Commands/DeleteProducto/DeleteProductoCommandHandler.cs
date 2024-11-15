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
            var producto = await _productoRepository.GetById(request.ProductoId);

            if (producto == null)
            {
                _logger.LogError($"{request.ProductoId} producto no existe en el sistma");
                throw new NotFoundException(nameof(producto), request.ProductoId);
            }

            // Si hay una imagen asociada, eliminarla del servidor
            if (!string.IsNullOrEmpty(producto.Imagen))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "products", "images", Path.GetFileName(producto.Imagen.TrimStart('/')));

                if (File.Exists(imagePath))
                {
                    try
                    {
                        File.Delete(imagePath);
                        _logger.LogInformation($"Imagen eliminada del servidor: {imagePath}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error al eliminar la imagen: {ex.Message}");
                        // Puedes optar por continuar o lanzar un error dependiendo de si quieres que falle la eliminación del producto si no se puede eliminar la imagen.
                    }
                }
                else
                {
                    _logger.LogWarning($"No se encontró la imagen en el servidor para eliminar: {imagePath}");
                }
            }

            await _productoRepository.DeleteAsync(producto);
            _logger.LogInformation($"El {request.ProductoId} fue eliminado con exito");

            return;
        }
    }
}
