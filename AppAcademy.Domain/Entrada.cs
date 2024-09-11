using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain
{
    public class Entrada
    {
        public string EntradaId { get; set; } = Guid.NewGuid().ToString();
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public int VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }

        // Relaciones
        public User? User { get; set; }
        public Ubicacion? Origen { get; set; }
    }
}
