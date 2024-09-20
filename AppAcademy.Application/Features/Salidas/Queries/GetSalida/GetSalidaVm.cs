namespace AppAcademy.Application.Features.Salidas.Queries.GetSalida
{
    public class GetSalidaVm
    {
        public DateTime FechaSalida { get; set; }
        public int TotalProductosSalida { get; set; }
        public string? Comentarios { get; set; }

        // Relaciones
        public string? UserId { get; set; }
    }
}
