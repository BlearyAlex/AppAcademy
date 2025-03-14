using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetMonthsAvailables
{
    public class GetAvailableMonthsQueryHandler : IRequestHandler<GetAvailableMonthsQuery, List<AvailableMonthDto>>
    {
        private readonly IAcademicCycleRepository _academicCycleRepository;
        private readonly IPaymentRepository _paymentRepository;

        public GetAvailableMonthsQueryHandler(IAcademicCycleRepository academicCycleRepository, IPaymentRepository paymentRepository)
        {
            _academicCycleRepository = academicCycleRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<List<AvailableMonthDto>> Handle(GetAvailableMonthsQuery request, CancellationToken cancellationToken)
        {
            var cycle = await _academicCycleRepository.GetByIdInt(request.AcademicCycleId);
            if (cycle == null)
            {
                throw new Exception("Ciclo académico no encontrado.");
            }

            // Llamar al método del repositorio para obtener los meses disponibles
            var availableMonths = await _academicCycleRepository.GetMonthsAvailable(request.StudentId, cycle);

            var result = availableMonths.Select(m => new AvailableMonthDto
            {
                Mes = m.mes,
                Anio = m.anio,
                Label = ConvertMonthNumberToName(m.mes)
            }).ToList();

            return result;
        }

        private string ConvertMonthNumberToName(int mes)
        {
            var months = new[]
            {
            "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
            "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
        };

            return mes >= 1 && mes <= 12 ? months[mes - 1] : "Desconocido";
        }
    }
}
