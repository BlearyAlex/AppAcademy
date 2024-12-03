using AppAcademy.Application.Contracts.Persistence;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVentaForDay
{
    public class GetVentaForDayQueryHandler : IRequestHandler<GetVentaForDayQuery, GetVentaForDayVm>
    {
        private readonly IVentaRepository _ventaRepository;

        public GetVentaForDayQueryHandler(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<GetVentaForDayVm> Handle(GetVentaForDayQuery request, CancellationToken cancellationToken)
        {
            return await _ventaRepository.GetVentaForDay();
        }
    }
}
