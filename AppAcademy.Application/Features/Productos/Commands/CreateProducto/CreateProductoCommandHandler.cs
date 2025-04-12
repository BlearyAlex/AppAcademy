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
        private readonly IFileStorageService _fileStorageService;

        public CreateProductoCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogger<CreateProductoCommandHandler> logger, IFileStorageService fileStorageService)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        public async Task<string> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la imagen y obtener la url
                    var imageUrl = await _fileStorageService.SaveImageAndGetUrl(request.ImageFile);
                    request.Imagen = imageUrl; // Almacenar la Url de la imagen en el modelo 
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la imagen del producto");
                }
            }

            var nuevoProducto = new Producto
            {
                Nombre = request.Nombre,
                CodigoBarras = request.CodigoBarras,
                Descripcion = request.Descripcion,
                Imagen = request.Imagen,
                Color = request.Color,
                Costo = request.Costo,
                Utilidad = request.Utilidad,
                Precio = request.Precio,
                EstadoProducto = request.EstadoProducto,
                Stock = request.Stock,
                FechaRegistro = DateTime.UtcNow,
                CategoriaId = request.CategoriaId,
                MarcaId = request.MarcaId,
                ProveedorId = request.ProveedorId
            };

            // Guardar el producto en la base de datos 
            var newProducto = await _productoRepository.AddAsync(nuevoProducto);

            // Registrar la creación del producto
            _logger.LogInformation($"Producto {newProducto.ProductoId} fue creado exitosamente");

            // Retornar el ID del nuevo producto
            return newProducto.ProductoId;
        }
    }
}
