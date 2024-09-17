using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Producto
    {
        public string ProductoId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? CodigoBarras { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal DescuentoBase { get; set; }
        public int Impuesto { get; set; }
        public ProductoEstado EstadoProducto { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }


        // Relaciones
        public List<HistorialInventario> HistorialInventarios { get; set; } = [];
        public List<Inventario> Inventarios { get; set; } = [];
        public List<EntradaProducto> EntradaProductos { get; set; } = [];
        public List<Devolucion> Devoluciones { get; set; } = [];
        public List<DetalleVenta> DetalleVentas { get; set; } = [];
        public List<HistorialPrecio> HistorialPrecios { get; set; } = [];
        public List<Promocion> Promociones { get; set; } = [];

        public string? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public string? MarcaId { get; set; }
        public Marca? Marca { get; set; }

        public string? ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}
