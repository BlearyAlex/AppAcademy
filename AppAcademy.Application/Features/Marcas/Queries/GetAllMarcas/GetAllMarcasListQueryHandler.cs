using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Inventarios.Queries.GetAllInventarios;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas
{
    public class GetAllMarcasListQueryHandler : IRequestHandler<GetAllMarcasListQuery, List<GetAllMarcasVm>>
    {
        private readonly IMarcaRepository _marcaRepository;
        private readonly IMapper _mapper;

        public GetAllMarcasListQueryHandler(IMarcaRepository marcaRepository, IMapper mapper)
        {
            _marcaRepository = marcaRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllMarcasVm>> Handle(GetAllMarcasListQuery request, CancellationToken cancellationToken)
        {
            var marcaList = await _marcaRepository.GetAllAsync();

            return _mapper.Map<List<GetAllMarcasVm>>(marcaList);
        }
    }
}
