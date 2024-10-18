
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas
{
    public class GetAllEntradasVm
    {
        public string EntradaId { get; set; }
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEmision { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }
        public List<EntradasProductoVm> Productos { get; set; }
    }

    public class EntradasProductoVm
    {
        public string EntradaProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public string ProductoId { get; set; }
    }
}
