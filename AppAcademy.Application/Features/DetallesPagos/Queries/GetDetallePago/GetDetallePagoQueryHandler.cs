using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetDetalleCorte;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesPagos.Queries.GetDetallePago
{
    public class GetDetallePagoQueryHandler : IRequestHandler<GetDetallePagoQuery, GetDetallePagoVm>
    {
        private readonly IDetallePagoRepository _repository;
        private readonly IMapper _mapper;

        public GetDetallePagoQueryHandler(IDetallePagoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetDetallePagoVm> Handle(GetDetallePagoQuery request, CancellationToken cancellationToken)
        {
            var detallePago = await _repository.GetById(request._DetallePagoId);

            return _mapper.Map<GetDetallePagoVm>(detallePago);
        }
    }
}
