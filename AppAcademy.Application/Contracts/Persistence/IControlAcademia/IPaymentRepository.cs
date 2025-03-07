using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface IPaymentRepository : IAsyncRepository<Payment>
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<bool> UpdatePaymentSaldo(Payment payment);
    }
}
