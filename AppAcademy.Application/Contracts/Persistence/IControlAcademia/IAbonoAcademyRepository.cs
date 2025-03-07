using AppAcademy.Domain.ControlAcademia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface IAbonoAcademyRepository : IAsyncRepository<AbonoAcademy>
    {
        Task<bool> CreateAbonoAcademy(int paymentId, decimal montoAbonado);
        Task<bool> DeleteAbono(int abonoId);
    }
}
