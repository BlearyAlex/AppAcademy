using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductoCommandHandler> _logger;
        private readonly IFileStorageService _fileStorageService;

        public UpdateProductoCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogger<UpdateProductoCommandHandler> logger, IFileStorageService fileStorageService)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        public async Task Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetById(request.ProductoId);

            if (producto == null)
            {
                _logger.LogError($"Producto con ID {request.ProductoId} no encontrado.");
                throw new ApplicationException("Producto no encontrado");
            }

            // Si se proporciona una nueva imagen
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    var oldImageUrl = producto.Imagen;

                    // Guardar la nueva imagen y obtener la URL
                    var imageUrl = await _fileStorageService.SaveImageAndGetUrl(request.ImageFile);
                    producto.Imagen = imageUrl;

                    if (!string.IsNullOrWhiteSpace(oldImageUrl))
                    {
                        // Puedes manejar el resultado o loguear si falla la eliminación
                        var deleted = await _fileStorageService.DeleteImage(oldImageUrl);
                        if (!deleted)
                        {
                            _logger.LogWarning($"No se pudo eliminar la imagen antigua: {oldImageUrl}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la nueva imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la nueva imagen del producto");
                }
            }

            producto.Nombre = request.Nombre;
            producto.CodigoBarras = request.CodigoBarras;
            producto.Descripcion = request.Descripcion;
            producto.Color = request.Color;
            producto.Costo = request.Costo;
            producto.Utilidad = request.Utilidad;
            producto.Precio = request.Precio;
            producto.EstadoProducto = request.EstadoProducto;
            producto.Stock = request.Stock;
            producto.CategoriaId = request.CategoriaId;
            producto.MarcaId = request.MarcaId;
            producto.ProveedorId = request.ProveedorId;

            await _productoRepository.UpdateAsync(producto);

            _logger.LogInformation($"Producto {producto.ProductoId} actualizado exitosamente");

        }
    }
}
