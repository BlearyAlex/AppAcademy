using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.EntradasProductos.Queries.GetEntrada;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.HistorialInventarios.Queries.GetHistorialInventario
{
    public class GetHistorialInventarioQueryHandler : IRequestHandler<GetHistorialInventarioQuery, GetHistorialInventarioVm>
    {
        private readonly IHistorialInventarioRepository _historialInventarioRepository;
        private readonly IMapper _mapper;

        public GetHistorialInventarioQueryHandler(IHistorialInventarioRepository historialInventarioRepository, IMapper mapper)
        {
            _historialInventarioRepository = historialInventarioRepository;
            _mapper = mapper;
        }

        public async Task<GetHistorialInventarioVm> Handle(GetHistorialInventarioQuery request, CancellationToken cancellationToken)
        {
            var historialInv = await _historialInventarioRepository.GetById(request._HistorialInventarioId);

            return _mapper.Map<GetHistorialInventarioVm>(historialInv);
        }
    }
}
