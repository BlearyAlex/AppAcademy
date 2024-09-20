using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Promociones.Queries.GetPromocion
{
    public class GetPromocionQueryHandler : IRequestHandler<GetPromocionQuery, GetPromocionVm>
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;

        public GetPromocionQueryHandler(IPromocionRepository promocionRepository, IMapper mapper)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
        }

        public async Task<GetPromocionVm> Handle(GetPromocionQuery request, CancellationToken cancellationToken)
        {
            var promocion = await _promocionRepository.GetById(request._PromocionId);

            return _mapper.Map<GetPromocionVm>(promocion);
        }
    }
}
