using MediatR;

namespace AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy
{
    public class CreateAbonoAcademyCommand : IRequest<CreateAbonoAcademyResult>
    {
        public int PaymentId { get; set; }
        public decimal MontoAbonado { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
