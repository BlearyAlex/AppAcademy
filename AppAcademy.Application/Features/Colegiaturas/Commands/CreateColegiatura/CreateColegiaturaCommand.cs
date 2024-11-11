using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Colegiaturas.Commands.CreateColegiatura
{
    public class CreateColegiaturaCommand :IRequest<string>
    {
        public DateTime FechaPago { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Anio { get; set; }
        public MesEstado Mes { get; set; }
        public string? Notas { get; set; }
        public ColegiaturaEstado EstadoColegiatura { get; set; }


        // Relaciones
        public string? EstudianteId { get; set; }
    }
}
