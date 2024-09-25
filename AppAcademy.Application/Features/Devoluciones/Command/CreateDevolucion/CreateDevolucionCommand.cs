using MediatR;

namespace AppAcademy.Application.Features.Devoluciones.Command.CreateDevolucion
{
    public class CreateDevolucionCommand : IRequest<string>
    {
        public decimal Cantidad { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        // Relaciones
        public string? VentaId { get; set; }
        public string? ProductoId { get; set; }
        public string? UserId { get; set; }
       
    }
}
