using MediatR;

namespace AppAcademy.Application.Features.Entradas.Commands.CreateEntrada
{
    public class CreateEntradaCommand : IRequest<string>
    {
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public int VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }
        public string? OrigenId { get; set; }
        public List<CreateEntradaProductoModel>? Productos { get; set; } = new List<CreateEntradaProductoModel>();
    }

    public class CreateEntradaProductoModel
    {
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public int ProductoId { get; set; }
    }
}
