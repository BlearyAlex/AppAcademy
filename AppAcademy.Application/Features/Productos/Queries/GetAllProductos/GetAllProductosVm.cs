namespace AppAcademy.Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosVm
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
        public string EstadoProducto { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relación con otras entidades
        public GetAllCategoriaVm? Categoria { get; set; }
        public GetAllMarcaVm? Marca { get; set; }
        public GetAllProveedorVm? Proveedor { get; set; }
    }

    public class GetAllCategoriaVm
    {
        public string CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetAllMarcaVm
    {
        public string MarcaId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetAllProveedorVm
    {
        public string ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

}
