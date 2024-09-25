namespace AppAcademy.Application.Features.EntradasProductos.Queries.GetEntrada
{
    public class GetEntradaProductoVm
    {
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        // Relaciones
        public string? EntradaId { get; set; }
        public string? ProductoId { get; set; }
    }
}
