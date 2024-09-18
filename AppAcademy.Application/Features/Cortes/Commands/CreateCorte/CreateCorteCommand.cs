using MediatR;

namespace AppAcademy.Application.Features.Cortes.Commands.CreateCorte
{
    public class CreateCorteCommand : IRequest<string>
    {
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
        public decimal TotalVales { get; set; }
        public decimal TotalDevoluciones { get; set; }
        public string? Comentarios { get; set; }

        public string? UserId { get; set; }
    }
}
