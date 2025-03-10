using MediatR;

namespace AppAcademy.Application.Features.Careers.Commands.CreateCareer
{
    public class CreateCareerCommand : IRequest<int>
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public decimal CostoMensual { get; set; }
        public string Color { get; set; }
        public bool Activa { get; set; }
    }
}
