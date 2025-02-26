using MediatR;

namespace AppAcademy.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest
    {
        public int PaymentId { get; set; }
        public DateTime FechaPago { get; set; }
        public int MesPagado { get; set; }  // 1 = Enero, 2 = Febrero...
        public int AñoPagado { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; }  // Efectivo, Tarjeta, Transferencia

        public int? StudentId { get; set; }
    }
}
