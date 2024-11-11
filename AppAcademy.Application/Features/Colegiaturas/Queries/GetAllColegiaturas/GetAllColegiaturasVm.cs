using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Queries.GetAllColegiaturas
{
    public class GetAllColegiaturasVm
    {
        public string ColegiaturaId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Anio { get; set; }
        public string Mes { get; set; }
        public string? Notas { get; set; }
        public string EstadoColegiatura { get; set; }


        // Relaciones
        public string? EstudianteId { get; set; }
        public string EstudianteNombre { get; set; }
    }
}
