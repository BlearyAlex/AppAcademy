using MediatR;

namespace AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada
{
    public class UpdateEntradaCommand : IRequest
    {
        public string EntradaId { get; set; }
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEmision { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }
        public List<UpdateEntradaProductoModel> Productos { get; set; } = new List<UpdateEntradaProductoModel>();
    }

    public class UpdateEntradaProductoModel
    {
        public string? EntradaProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public string EntradaId { get; set; }
        public string ProductoId { get; set; }
    }
}
