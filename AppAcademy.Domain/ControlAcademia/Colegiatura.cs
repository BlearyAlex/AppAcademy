using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Colegiatura
    {
        public string ColegiaturaId { get; set; } = Guid.NewGuid().ToString();
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
        public Estudiante? Estudiante { get; set; }
    }
}
