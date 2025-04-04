using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Bitacora.Queries.GetBitacoras
{
    public class GetBitacorasListQueryHandler : IRequestHandler<GetBitacorasListQuery, List<GetBitacorasVm>>
    {
        private readonly IBitacoraRepository _bitacoraRepository;
        private readonly IMapper _mapper;

        public GetBitacorasListQueryHandler(IBitacoraRepository bitacoraRepository, IMapper mapper)
        {
            _bitacoraRepository = bitacoraRepository;
            _mapper = mapper;
        }

        public async Task<List<GetBitacorasVm>> Handle(GetBitacorasListQuery request, CancellationToken cancellationToken)
        {
            var bitacoras = await _bitacoraRepository.GetAllBitacoras();

            return _mapper.Map<List<GetBitacorasVm>>(bitacoras);
        }
    }
}
