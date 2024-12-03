using AppAcademy.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVentaForMonth
{
    public class GetVentaForMonthQueryHandler : IRequestHandler<GetVentaForMonthQuery, GetVentaForMonthVm>
    {
        private readonly IVentaRepository _ventaRepository;

        public GetVentaForMonthQueryHandler(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<GetVentaForMonthVm> Handle(GetVentaForMonthQuery request, CancellationToken cancellationToken)
        {
            return await _ventaRepository.GetVentaForMont();
        }
    }
}
