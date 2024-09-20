using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Salidas.Queries.GetSalida
{
    public class GetSalidaQueryHandler : IRequestHandler<GetSalidaQuery, GetSalidaVm>
    {
        private readonly ISalidaRepository _salidaRepository;
        private readonly IMapper _mapper;

        public GetSalidaQueryHandler(ISalidaRepository salidaRepository, IMapper mapper)
        {
            _salidaRepository = salidaRepository;
            _mapper = mapper;
        }

        public async Task<GetSalidaVm> Handle(GetSalidaQuery request, CancellationToken cancellationToken)
        {
            var proveedor = await _salidaRepository.GetById(request._SalidaId);

            return _mapper.Map<GetSalidaVm>(proveedor);
        }
    }
}
