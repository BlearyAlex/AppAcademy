using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.EntradasProductos.Queries.GetAllEntradas
{
    public class GetAllEntradasProductosListQueryHandler : IRequestHandler<GetAllEntradasProductosListQuery, List<GetAllEntradasProductosVm>>
    {
        private readonly IEntradaProductoRepository _entradaProductoRepository;
        private readonly IMapper _mapper;

        public GetAllEntradasProductosListQueryHandler(IEntradaProductoRepository entradaProductoRepository, IMapper mapper)
        {
            _entradaProductoRepository = entradaProductoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllEntradasProductosVm>> Handle(GetAllEntradasProductosListQuery request, CancellationToken cancellationToken)
        {
            var detalleEntradaList = await _entradaProductoRepository.GetAllAsync();

            return _mapper.Map<List<GetAllEntradasProductosVm>>(detalleEntradaList);
        }
    }
}
