using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoCommand : IRequest
    {
        public string ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal DescuentoBase { get; set; }
        public int Impuesto { get; set; }
        public ProductoEstado EstadoProducto { get; set; } = ProductoEstado.Alta;
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        // Relación con otras entidades
        public string? CategoriaId { get; set; }
        public string? MarcaId { get; set; } = string.Empty;
        public string? ProveedorId { get; set; } = string.Empty;

    }
}
