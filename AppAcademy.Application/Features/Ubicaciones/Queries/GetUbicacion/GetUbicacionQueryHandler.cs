using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Ubicaciones.Queries.GetUbicacion
{
    public class GetUbicacionQueryHandler : IRequestHandler<GetUbicacionQuery, GetUbicacionVm>
    {
        private readonly IUbicacionRepository _repository;
        private readonly IMapper _mapper;

        public GetUbicacionQueryHandler(IUbicacionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetUbicacionVm> Handle(GetUbicacionQuery request, CancellationToken cancellationToken)
        {
            var ubicacion = await _repository.GetById(request._UbicacionId);

            return _mapper.Map<GetUbicacionVm>(ubicacion);
        }
    }
}
