using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoCommand : IRequest<string>
    {
        public string Nombre { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public decimal Costo { get; set; } = 0;
        public decimal Precio { get; set; } = 0;
        public decimal DescuentoBase { get; set; } = 0;
        public int Impuesto { get; set; } = 16;
        public ProductoEstado EstadoProducto { get; set; } = ProductoEstado.Alta;
        public int StockMinimo { get; set; } = 0;
        public int StockMaximo { get; set; } = 10;

        // Relación con otras entidades
        public string? CategoriaId { get; set; }
        public string? MarcaId { get; set; }
        public string? ProveedorId { get; set; }
    }
}
