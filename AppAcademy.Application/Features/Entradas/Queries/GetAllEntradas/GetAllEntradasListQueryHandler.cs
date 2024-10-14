using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas
{
    public class GetAllEntradasListQueryHandler : IRequestHandler<GetAllEntradasListQuery, List<GetAllEntradasVm>>
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IMapper _mapper;

        public GetAllEntradasListQueryHandler(IEntradaRepository entradaRepository, IMapper mapper)
        {
            _entradaRepository = entradaRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllEntradasVm>> Handle(GetAllEntradasListQuery request, CancellationToken cancellationToken)
        {
            var entradas = await _entradaRepository.GetEntradasWithProductos();
            
            return entradas.Select(e => new GetAllEntradasVm
            {
                EntradaId = e.EntradaId,
                FechaDeEntrega = e.FechaDeEntrega,
                NumeroFactura = e.NumeroFactura,
                VencimientoPago = e.VencimientoPago,
                Folio = e.Folio,
                Bruto = e.Bruto,
                Productos = e.EntradaProductos.Select(p => new EntradaProductoVm
                {
                    EntradaProductoId = p.EntradaProductoId,
                    Cantidad = p.Cantidad,
                    Costo = p.Costo,
                    ProductoId = p.ProductoId
                }).ToList()
            }).ToList();
        }
    }
}
