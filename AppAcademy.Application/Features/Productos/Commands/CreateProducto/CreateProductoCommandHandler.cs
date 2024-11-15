using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, string>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductoCommandHandler> _logger;

        public CreateProductoCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogger<CreateProductoCommandHandler> logger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la imagen y obtener la url
                    var imageUrl = await SaveImageAndGetUrl(request.ImageFile);
                    request.Imagen = imageUrl; // Almacenar la Url de la imagen en el modelo 
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la imagen del producto");
                }
            }

            // Mapear el comando a la entidad de producto 
            var producto = _mapper.Map<Producto>(request);

            // Guardar el producto en la base de datos 
            var newProducto = await _productoRepository.AddAsync(producto);

            // Registrar la creación del producto
            _logger.LogInformation($"Producto {newProducto.ProductoId} fue creado exitosamente");

            // Retornar el ID del nuevo producto
            return newProducto.ProductoId;
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
