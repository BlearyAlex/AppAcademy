using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommandHandler : IRequestHandler<CreateVentaCommand, string>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVentaCommandHandler> _logger;

        public CreateVentaCommandHandler(IVentaRepository ventaRepository, IProductoRepository productoRepository, IMapper mapper, ILogger<CreateVentaCommandHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
        {
            var nuevaVenta = new Venta
            {
                FechaCompra = DateTime.Now,
                EstadoVenta = request.EstadoVenta,
                ClienteId = request.ClienteId,
                Bruto = request.Bruto,
                TotalProductos = request.TotalProductos,
                EstadoTipoPago = request.EstadoTipoPago,
            };

            if(request.Productos != null && request.Productos.Count > 0)
            {
                foreach(var product in request.Productos)
                {
                    var nuevoDetalleVenta = new DetalleVenta
                    {
                        Costo = product.Costo,
                        Cantidad = product.Cantidad,
                        ProductoId = product.ProductoId,
                    };

                    nuevaVenta.DetalleVentas.Add(nuevoDetalleVenta);

                    var productoEntidad = await _productoRepository.GetById(product.ProductoId);

                    if(productoEntidad != null)
                    {
                        productoEntidad.StockMinimo -= product.Cantidad;
                        
                        await _productoRepository.UpdateAsync(productoEntidad);
                    }
                }
            }

            var ventaId = await _ventaRepository.CreateVentaWithProduct(nuevaVenta);

            return ventaId;
        }
    }
}
