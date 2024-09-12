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
