namespace AppAcademy.Application.Features.Productos.Queries.GetProductsByName
{
    public class GetProductsByNameVm
    {
        public string? Nombre { get; set; }
        public string? CodigoBarras { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal DescuentoBase { get; set; }
        public int Impuesto { get; set; }
        public Estado EstadoProducto { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        public enum Estado
        {
            Alta,
            Baja
        }
    }
}
