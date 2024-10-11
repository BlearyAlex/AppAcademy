using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Devoluciones.Queries.GetAllDevoluciones;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var entradaList = await _entradaRepository.GetAllAsync();

            return _mapper.Map<List<GetAllEntradasVm>>(entradaList);
        }
    }
}
