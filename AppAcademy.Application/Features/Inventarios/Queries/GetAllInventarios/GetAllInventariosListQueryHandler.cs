using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Queries.GetAllInventarios
{
    public class GetAllInventariosListQueryHandler : IRequestHandler<GetAllInventariosListQuery, List<GetAllInventariosVm>>
    {
        private readonly IInventarioRepository _inventarioRepository;
        private readonly IMapper _mapper;

        public GetAllInventariosListQueryHandler(IInventarioRepository inventarioRepository, IMapper mapper)
        {
            _inventarioRepository = inventarioRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllInventariosVm>> Handle(GetAllInventariosListQuery request, CancellationToken cancellationToken)
        {
            var inventarioList = await _inventarioRepository.GetAllAsync();

            return _mapper.Map<List<GetAllInventariosVm>>(inventarioList);
        }
    }
}
