using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using static AppAcademy.Application.Features.Entradas.Queries.GetEntrada.GetEntradaVm;

namespace AppAcademy.Application.Features.Entradas.Queries.GetEntrada
{
    public class GetEntradaQueryHandler : IRequestHandler<GetEntradaQuery, GetEntradaVm>
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IMapper _mapper;

        public GetEntradaQueryHandler(IEntradaRepository entradaRepository, IMapper mapper)
        {
            _entradaRepository = entradaRepository;
            _mapper = mapper;
        }

        public async Task<GetEntradaVm> Handle(GetEntradaQuery request, CancellationToken cancellationToken)
        {
            var entrada = await _entradaRepository.GetEntradaByIdWithProductsAsync(request._EntradaId);

            if(entrada == null)
            {
                throw new NotFoundException(nameof(Entrada), request._EntradaId);
            }

            return new GetEntradaVm
            {
                EntradaId = entrada.EntradaId,
                TotalProductosEntrada = entrada.TotalProductosEntrada,
                FechaDeEmision = entrada.FechaDeEmision,
                NumeroFactura = entrada.NumeroFactura,
                Folio = entrada.Folio,
                Bruto = entrada.Bruto,
                Productos = entrada.EntradaProductos.Select(p => new EntradaProductoVm
                {
                    EntradaProductoId = p.EntradaProductoId,
                    Cantidad = p.Cantidad,
                    Costo = p.Costo,
                    ProductoId = p.ProductoId,
                    NombreProducto = p.Producto.Nombre
                }).ToList()
            };
        }
    }
}
