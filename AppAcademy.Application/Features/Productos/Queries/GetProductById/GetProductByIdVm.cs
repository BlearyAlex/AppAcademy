using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductById
{
    public class GetProductByIdVm
    {
        public string ProductoId { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarras { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal Costo { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Precio { get; set; }
        public decimal DescuentoBase { get; set; }
        public int Impuesto { get; set; }
        public ProductoEstado EstadoProducto { get; set; }
        public int StockMinimo { get; set; }

        // Relación con otras entidades
        public string? CategoriaId { get; set; }
        public string? MarcaId { get; set; }
        public string? ProveedorId { get; set; }
    }
}