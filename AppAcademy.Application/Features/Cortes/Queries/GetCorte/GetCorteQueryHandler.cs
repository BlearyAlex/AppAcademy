using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Clientes.Queries.GetClienteById;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Queries.GetCorte
{
    public class GetCorteQueryHandler : IRequestHandler<GetCorteQuery, GetCorteVm>
    {
        private readonly ICorteRepository _corteRepository;
        private readonly IMapper _mapper;

        public GetCorteQueryHandler(ICorteRepository corteRepository, IMapper mapper)
        {
            _corteRepository = corteRepository;
            _mapper = mapper;
        }

        public async Task<GetCorteVm> Handle(GetCorteQuery request, CancellationToken cancellationToken)
        {
            var corte = await _corteRepository.GetById(request._CorteId);

            return _mapper.Map<GetCorteVm>(corte);
        }
    }
}
