using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class PaymentRepository : AsyncRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
