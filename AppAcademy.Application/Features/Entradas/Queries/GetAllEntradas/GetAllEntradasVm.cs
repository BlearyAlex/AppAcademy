
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas
{
    public class GetAllEntradasVm
    {
        public string EntradaId { get; set; }
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }
        public List<EntradaProductoVm> Productos { get; set; }
    }

    public class EntradaProductoVm
    {
        public string EntradaProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public string ProductoId { get; set; }
    }
}
