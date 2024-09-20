using MediatR;

namespace AppAcademy.Application.Features.Salidas.Command.UpdateSalida
{
    public class UpdateSalidaCommand : IRequest
    {
        public string SalidaId { get; set; }
        public DateTime FechaSalida { get; set; }
        public int TotalProductosSalida { get; set; }
        public string? Comentarios { get; set; }
    }
}
