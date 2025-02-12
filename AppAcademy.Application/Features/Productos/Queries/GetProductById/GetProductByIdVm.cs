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
        public string Color { get; set; }
        public ProductoEstado EstadoProducto { get; set; }
        public int Stock { get; set; }

        // Relación con otras entidades
        public GetByIdCategoriaVm Categoria { get; set; }
        public GetByIdMarcaVm Marca { get; set; }
        public GetByIdProveedorVm Proveedor { get; set; }
    }

    public class GetByIdCategoriaVm
    {
        public string CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetByIdMarcaVm
    {
        public string MarcaId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetByIdProveedorVm
    {
        public string ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}