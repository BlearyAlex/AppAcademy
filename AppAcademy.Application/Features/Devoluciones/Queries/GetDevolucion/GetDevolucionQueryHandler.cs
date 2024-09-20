using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetDetallePago;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Queries.GetDevolucion
{
    public class GetDevolucionQueryHandler : IRequestHandler<GetDevolucionQuery, GetDevolucionVm>
    {
        private readonly IDevolucionRepository _repository;
        private readonly IMapper _mapper;

        public GetDevolucionQueryHandler(IDevolucionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetDevolucionVm> Handle(GetDevolucionQuery request, CancellationToken cancellationToken)
        {
            var devolucion = await _repository.GetById(request._DevolucionId);

            return _mapper.Map<GetDevolucionVm>(devolucion);
        }
    }
}
