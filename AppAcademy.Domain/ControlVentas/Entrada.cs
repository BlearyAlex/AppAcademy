using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Entrada
    {
        public string EntradaId { get; set; } = Guid.NewGuid().ToString();
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }

        // Relaciones
        public string? UserId { get; set; }
        public User? User { get; set; }

        public string? OrigenId { get; set; }
        public Ubicacion? Origen { get; set; }

        public List<EntradaProducto> EntradaProductos { get; set; } = new List<EntradaProducto>();
    }
}
