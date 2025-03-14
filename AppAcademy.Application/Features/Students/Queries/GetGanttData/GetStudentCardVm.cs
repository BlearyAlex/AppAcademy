using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Students.Queries.GetGanttData
{
    public class GetStudentCardVm
    {
        public int StudentId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Foto { get; set; }
        public string CarreraNombre { get; set; }
        public int DuracionCarrera { get; set; }
        public int AnioPago { get; set; }
        public List<PaymentMonthDto> Pagos { get; set; }
    }
    public class PaymentMonthDto
    {
        public string Mes { get; set; }
        public string Pagado { get; set; }
        public int Anio { get; set; }
    }
}
