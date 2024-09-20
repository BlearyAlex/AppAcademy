using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Queries.GetInventario
{
    public class GetInventarioQueryHandler : IRequestHandler<GetInventarioQuery, GetInventarioVm>
    {
        private readonly IInventarioRepository _repository;
        private readonly IMapper _mapper;

        public GetInventarioQueryHandler(IInventarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetInventarioVm> Handle(GetInventarioQuery request, CancellationToken cancellationToken)
        {
            var inventario = await _repository.GetById(request._InventarioId);

            return _mapper.Map<GetInventarioVm>(inventario);
        }
    }
}
