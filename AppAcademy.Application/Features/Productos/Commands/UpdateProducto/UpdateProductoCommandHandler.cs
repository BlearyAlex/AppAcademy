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

        public UpdateProductoCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogger<UpdateProductoCommandHandler> logger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
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
            if(request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la nueva imagen y obtener la URL
                    var imageUrl = await SaveImageAndGetUrl(request.ImageFile);
                    producto.Imagen = imageUrl;
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

        private async Task<string> SaveImageAndGetUrl(IFormFile imageFile)
        {
            try
            {
                // Generar un nombre único para la imagen
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

                // Crear directorio si no existe
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                // Guardar la imagen en el servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Devolver la URL de la imagen
                return $"/images/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar la imagen: {ex.Message}");
                throw new ApplicationException("Error al guardar la imagen del producto");
            }
        }
    }
}
