using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVenta
{
    public class GetVentaQueryHandler : IRequestHandler<GetVentaQuery, GetVentaVm>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;

        public GetVentaQueryHandler(IVentaRepository ventaRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _mapper = mapper;
        }

        public async Task<GetVentaVm> Handle(GetVentaQuery request, CancellationToken cancellationToken)
        {
            var venta = await _ventaRepository.GetVentaByIdWithProductsAsync(request._VentaId);

            return new GetVentaVm
            {
                VentaId = venta.VentaId,
                FechaCompra = DateTime.Now,
                EstadoVenta = venta.EstadoVenta,
                EstadoTipoPago = venta.EstadoTipoPago,
                ClienteId = venta.ClienteId,
                Bruto = venta.Bruto,
                Descuento = venta.Descuento,
                Neto = venta.Neto,
                TotalProductos = venta.TotalProductos,
                DetalleVentas = venta.DetalleVentas.Select(v => new GetDetallesVentaVm
                {
                    DetalleVentaId = v.DetalleVentaId,
                    Cantidad = v.Cantidad,
                    Costo = v.Costo,
                    ProductoId = v.ProductoId,
                    NombreProducto = v.Producto.Nombre
                }).ToList()
            };
        }
    }
}
