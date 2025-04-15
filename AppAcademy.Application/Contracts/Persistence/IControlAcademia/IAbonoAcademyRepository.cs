using AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy;
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
        Task<CreateAbonoAcademyResult> CreateAbonoAcademy(int paymentId, decimal montoAbonado, string userName);
        Task<bool> DeleteAbono(int abonoId, string userName);
    }
}
