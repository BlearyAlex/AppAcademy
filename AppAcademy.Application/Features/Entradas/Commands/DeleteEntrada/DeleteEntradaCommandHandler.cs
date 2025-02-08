using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada
{
    public class DeleteEntradaCommandHandler : IRequestHandler<DeleteEntradaCommand>
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEntradaCommandHandler> _logger;

        public DeleteEntradaCommandHandler(IEntradaRepository entradaRepository, IProductoRepository productoRepository, IMapper mapper, ILogger<DeleteEntradaCommandHandler> logger)
        {
            _entradaRepository = entradaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteEntradaCommand request, CancellationToken cancellationToken)
        {
            var entrada = await _entradaRepository.GetEntradaByIdWithProductsAsync(request.EntradaId);

            if (entrada == null)
            {
                _logger.LogError($"Entrada with id {request.EntradaId} not found.");
                throw new NotFoundException(nameof(Entrada), request.EntradaId);
            }

            // Restamos las cantidades del stock de los productos asociados a la entrada
            foreach (var entradaProducto in entrada.EntradaProductos)
            {
                var producto = await _productoRepository.GetById(entradaProducto.ProductoId);

                if (producto != null)
                {
                    // Verificamos si el stock será negativo después de la operación
                    if (producto.Stock< entradaProducto.Cantidad)
                    {
                        // Si el stock sería negativo, solo ajustamos el stock para que llegue a cero
                        _logger.LogWarning($"El stock del producto {producto.Nombre} (ID: {producto.ProductoId}) es insuficiente para restar la cantidad solicitada. Ajustando el stock a cero.");
                        producto.Stock = 0;  // Ajustamos el stock a cero en lugar de permitir que sea negativo
                    }
                    else
                    {
                        // Si el stock es suficiente, restamos la cantidad normalmente
                        producto.Stock -= entradaProducto.Cantidad;
                    }

                    // Guardamos los cambios en el repositorio de productos
                    await _productoRepository.UpdateAsync(producto);
                }
            }

            await _entradaRepository.DeleteEntrada(entrada.EntradaId);  
        }
    }
}
