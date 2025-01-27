namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Salida
    {
        public string SalidaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime FechaSalida { get; set; }
        public int TotalProductosSalida { get; set; }
        public string? Comentarios { get; set; }
    }
}
