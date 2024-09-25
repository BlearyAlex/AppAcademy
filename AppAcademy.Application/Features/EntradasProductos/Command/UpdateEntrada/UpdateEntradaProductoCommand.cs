using MediatR;

namespace AppAcademy.Application.Features.EntradasProductos.Command.UpdateEntrada
{
    public class UpdateEntradaProductoCommand : IRequest
    {
        public string EntradaProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        // Relaciones
        public string? EntradaId { get; set; }
        public string? ProductoId { get; set; }

    }
}
