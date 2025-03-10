using AppAcademy.Application.Features.Payments.Queries.GetPayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayments;
using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface IPaymentRepository : IAsyncRepository<Payment>
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<bool> UpdatePaymentSaldo(Payment payment);
        Task<List<GetPaymentsVm>> GetPayments();
        Task<GetPaymentVm> GetPayment(int paymentId);
    }
}
