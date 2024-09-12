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
        public CreateProductoEstado EstadoProducto { get; set; } = CreateProductoEstado.Alta;
        public int StockMinimo { get; set; } = 0;
        public int StockMaximo { get; set; } = 10;

        public enum CreateProductoEstado
        {
            Alta,
            Baja
        }
    }
}
