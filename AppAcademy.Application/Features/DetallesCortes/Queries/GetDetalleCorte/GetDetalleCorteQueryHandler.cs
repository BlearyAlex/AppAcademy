using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Queries.GetDetalleCorte
{
    public class GetDetalleCorteQueryHandler : IRequestHandler<GetDetalleCorteQuery, GetDetalleCorteVm>
    {
        private readonly IDetalleCorteRepository _repository;
        private readonly IMapper _mapper;

        public GetDetalleCorteQueryHandler(IDetalleCorteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetDetalleCorteVm> Handle(GetDetalleCorteQuery request, CancellationToken cancellationToken)
        {
            var detalleCorte = await _repository.GetById(request._DetalleCorteId);

            return _mapper.Map<GetDetalleCorteVm>(detalleCorte);
        }
    }
}
