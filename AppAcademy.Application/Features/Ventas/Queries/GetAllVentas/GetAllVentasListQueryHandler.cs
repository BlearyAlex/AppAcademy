using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasListQueryHandler : IRequestHandler<GetAllVentasListQuery, List<GetAllVentasVm>>
    {
        private readonly IVentaRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVentasListQueryHandler(IVentaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllVentasVm>> Handle(GetAllVentasListQuery request, CancellationToken cancellationToken)
        {
            var ventas = await _repository.GetVentasWithProductos();

            return ventas.Select(v => new GetAllVentasVm
            {
                VentaId = v.VentaId,
                FechaCompra = v.FechaCompra,
                EstadoVenta = v.EstadoVenta.ToString(), // Asegúrate de que esté aquí
                EstadoTipoPago = v.EstadoTipoPago.ToString(), // Asegúrate de que esté aquí
                ClienteId = v.ClienteId,
                Bruto = v.Bruto,
                Descuento = v.Descuento,
                Neto = v.Neto,
                TotalProductos = v.TotalProductos,
                DetalleVentas = v.DetalleVentas.Select(d => new GetAllDetallesVentaVm
                {
                    DetalleVentaId = d.DetalleVentaId,
                    Costo = d.Costo,
                    Cantidad = d.Cantidad,
                    ProductoId = d.ProductoId,
                }).ToList()
            }).ToList();
        }
    }
}
