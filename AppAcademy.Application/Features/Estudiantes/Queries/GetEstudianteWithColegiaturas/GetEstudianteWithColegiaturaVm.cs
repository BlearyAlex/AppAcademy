using AppAcademy.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteWithColegiaturas
{
    public class GetEstudianteWithColegiaturaVm
    {
        public string EstudianteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string Telefono { get; set; }
        public string? Direccion { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }
        public List<GetColegiaturaWithEstudianteVm> Colegiaturas { get; set; }
    }
    
    public class GetColegiaturaWithEstudianteVm
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
    }
}
