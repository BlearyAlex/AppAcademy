namespace AppAcademy.Application.Features.Entradas.Queries.GetEntrada
{
    public class GetEntradaVm
    {
        public string EntradaId { get; set; }
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEmision { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }
        public List<EntradaProductoVm> Productos { get; set; }

        public class EntradaProductoVm
        {
            public string EntradaProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal Costo { get; set; }
            public string ProductoId { get; set; }
            public string NombreProducto { get; set; }
        }
    }
}
