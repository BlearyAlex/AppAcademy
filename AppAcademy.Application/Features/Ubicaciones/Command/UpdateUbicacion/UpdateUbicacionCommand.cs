using MediatR;

namespace AppAcademy.Application.Features.Ubicaciones.Command.UpdateUbicacion
{
    public class UpdateUbicacionCommand : IRequest
    {
        public string UbicacionId { get; set; } 
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
    }
}
