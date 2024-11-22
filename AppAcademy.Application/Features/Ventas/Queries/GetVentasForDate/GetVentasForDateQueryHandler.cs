using AppAcademy.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVentasForDate
{
    public class GetVentasForDateQueryHandler : IRequestHandler<GetVentasForDateQuery, List<GetVentasForDateVm>>
    {
        private readonly IVentaRepository _ventaRepository;

        public GetVentasForDateQueryHandler(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<List<GetVentasForDateVm>> Handle(GetVentasForDateQuery request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Periodo))
            {
                throw new ArgumentException("El periodo no puede ser nulo o vacío. Usa 'dia', 'semana' o 'mes'.");
            }

            return await _ventaRepository.GetVentasForDate(cancellationToken, request.Periodo);
        }
    }
}
