using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Queries.GetAllDevoluciones
{
    public class GetAllDevolucionesListQueryHandler : IRequestHandler<GetAllDevolucionesListQuery, List<GetAllDevolucionesVm>>
    {
        private readonly IDevolucionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDevolucionesListQueryHandler(IDevolucionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllDevolucionesVm>> Handle(GetAllDevolucionesListQuery request, CancellationToken cancellationToken)
        {
            var detalleDevolucion = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllDevolucionesVm>>(detalleDevolucion);
        }
    }
}
