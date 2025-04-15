using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Productos.Queries.GetAllProductosFilter
{
    public class GetAllProductosFilterVm
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
}
