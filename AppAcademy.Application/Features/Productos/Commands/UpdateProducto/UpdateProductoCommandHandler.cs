using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
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
            var productoUpdate = await _productoRepository.GetById(request.ProductoId);

            if(productoUpdate == null)
            {
                _logger.LogError($"No se encontro el id del producto {request.ProductoId}");
                throw new NotFoundException(nameof(Producto), request.ProductoId);
            }

            _mapper.Map(request, productoUpdate, typeof(UpdateProductoCommand), typeof(Producto));

            await _productoRepository.UpdateAsync(productoUpdate);

            _logger.LogInformation($"La operacion fue exitosa {request.ProductoId}");
        }
    }
}
