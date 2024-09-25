using MediatR;

namespace AppAcademy.Application.Features.EntradasProductos.Command.CreateEntrada
{
    public class CreateEntradaProductoCommand : IRequest<string>
    {
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        // Relaciones
        public string? EntradaId { get; set; }
        public string? ProductoId { get; set; }
    }
}
