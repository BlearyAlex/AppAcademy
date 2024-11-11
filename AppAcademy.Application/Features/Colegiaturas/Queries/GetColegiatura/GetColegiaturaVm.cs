using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Queries.GetColegiatura
{
    public class GetColegiaturaVm
    {
        public string ColegiaturaId { get; set; }
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
