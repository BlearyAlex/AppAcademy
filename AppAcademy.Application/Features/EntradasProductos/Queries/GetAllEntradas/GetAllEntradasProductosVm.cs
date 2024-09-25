namespace AppAcademy.Application.Features.EntradasProductos.Queries.GetAllEntradas
{
    public class GetAllEntradasProductosVm
    {
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        // Relaciones
        public string? EntradaId { get; set; }
        public string? ProductoId { get; set; }
    }
}
