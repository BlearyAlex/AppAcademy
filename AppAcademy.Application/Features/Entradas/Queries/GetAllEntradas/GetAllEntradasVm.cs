
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas
{
    public class GetAllEntradasVm
    {
        public string EntradaId { get; set; }
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public int VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }
        public string? OrigenId { get; set; }
        public int MyProperty { get; set; }
        public List<EntradaProducto> EntradaProductos { get; set; }
    }
}
