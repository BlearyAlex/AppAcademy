using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Queries.GetAllUbicaciones
{
    public class GetAllUbicacionesListQueryHandler : IRequestHandler<GetAllUbicacionesListQuery, List<GetAllUbicacionesVm>>
    {
        private readonly IUbicacionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUbicacionesListQueryHandler(IUbicacionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUbicacionesVm>> Handle(GetAllUbicacionesListQuery request, CancellationToken cancellationToken)
        {
            var ubicacionList = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllUbicacionesVm>>(ubicacionList);
        }
    }
}
