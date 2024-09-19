using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes
{
    public class GetAllDetallesCortesListQueryHandler : IRequestHandler<GetAllDetallesCortesListQuery, List<GetAllDetallesCortesVm>>
    {
        private readonly IDetalleCorteRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDetallesCortesListQueryHandler(IDetalleCorteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllDetallesCortesVm>> Handle(GetAllDetallesCortesListQuery request, CancellationToken cancellationToken)
        {
            var detalleCorteList = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllDetallesCortesVm>>(detalleCorteList);
        }
    }
}
