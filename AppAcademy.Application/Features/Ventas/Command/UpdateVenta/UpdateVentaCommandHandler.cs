using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Ventas.Command.UpdateVenta
{
    public class UpdateVentaCommandHandler : IRequestHandler<UpdateVentaCommand>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVentaCommandHandler> _logger;

        public UpdateVentaCommandHandler(IVentaRepository ventaRepository, IProductoRepository productoRepository, IMapper mapper, ILogger<UpdateVentaCommandHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        } 

        public async Task Handle(UpdateVentaCommand request, CancellationToken cancellationToken)
        {
            var ventaUpdate = await _ventaRepository.GetVentaByIdWithProductsAsync(request.VentaId);
            if (ventaUpdate == null)
            {
                throw new NotFoundException(nameof(Venta), request.VentaId);
            }

            var mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            var mexicoTime = TimeZoneInfo.ConvertTime(DateTime.Now, mexicoTimeZone);

            // Actualizar los datos de la venta
            ventaUpdate.Descuento = request.Descuento;
            ventaUpdate.Neto = request.Neto;
            ventaUpdate.Bruto = request.Bruto;
            ventaUpdate.EstadoVenta = request.EstadoVenta;
            ventaUpdate.EstadoTipoPago = request.EstadoTipoPago;
            ventaUpdate.FechaCompra = mexicoTime;
            ventaUpdate.ClienteId = request.ClienteId;

            // Actualizar los productos asociados
            // Primero limpiamos la lista actual de productos
            var productosEliminar = ventaUpdate.DetalleVentas.ToList();

            // Iterar sobre los productos antes de la actualizacion
            foreach ( var product in request.Productos)
            {
                // Verificar si es un producto existente o nuevo
                if (string.IsNullOrEmpty(product.DetalleVentaId))
                {
                    // Si es nuevo lo agregamos
                    var nuevoDetalleVenta = new DetalleVenta
                    {
                        Cantidad = product.Cantidad,
                        Costo = product.Costo,
                        ProductoId = product.ProductoId,
                        VentaId = product.VentaId,
                    };

                    ventaUpdate.DetalleVentas.Add(nuevoDetalleVenta);

                    // Actualizar el stock del producto
                    var producto = await _productoRepository.GetById(product.ProductoId);
                    if (producto != null)
                    {
                        producto.Stock -= product.Cantidad;
                        await _productoRepository.UpdateAsync(producto);
                    }
                }
                else
                {
                    // Si el producto ya existe, actualizamos los campos
                    var productoExistente = ventaUpdate.DetalleVentas
                        .FirstOrDefault(p => p.DetalleVentaId == product.DetalleVentaId);

                    if (productoExistente != null)
                    {
                        // Actualiza solo los campos necesarios
                        var cantidadAnterior = productoExistente.Cantidad;  // Guarda la cantidad anterior para calcular el cambio en el stock
                        productoExistente.Cantidad = product.Cantidad;
                        productoExistente.Costo = product.Costo;

                        // Actualizamos el stock del producto
                        var productoEntidad = await _productoRepository.GetById(product.ProductoId);
                        if (productoEntidad != null)
                        {
                            productoEntidad.Stock += (product.Cantidad - cantidadAnterior);
                            await _productoRepository.UpdateAsync(productoEntidad);
                        }

                        // Elimina de la lista de productos a eliminar
                        productosEliminar.Remove(productoExistente);
                    }
                }
            }

            // Eliminar los productos que ya no están en la lista
            foreach (var productoParaEliminar in productosEliminar)
            {
                await _ventaRepository.DeleteDetalleVentaAsync(productoParaEliminar);
            }

            // Guardar los cambios en el repositorio
            await _ventaRepository.UpdateAsync(ventaUpdate);

        }
    }
}
