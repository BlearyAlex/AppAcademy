using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Devoluciones.Queries.GetDevolucion;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var entrada = await _entradaRepository.GetById(request._EntradaId);

            return _mapper.Map<GetEntradaVm>(entrada);
        }
    }
}
