using AppAcademy.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AppAcademy.Application.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoCommand : IRequest
    {
        public string ProductoId { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoBarras { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public IFormFile? ImageFile { get; set; }
        public decimal Costo { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Precio { get; set; }
        public string Color { get; set; }
        public ProductoEstado EstadoProducto { get; set; }
        public int Stock { get; set; }

        // Relación con otras entidades
        public string? CategoriaId { get; set; }
        public string? MarcaId { get; set; } = string.Empty;
        public string? ProveedorId { get; set; } = string.Empty;

    }
}
