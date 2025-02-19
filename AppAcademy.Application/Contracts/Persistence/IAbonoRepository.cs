using AppAcademy.Domain.ControlVentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IAbonoRepository : IAsyncRepository<Abono>
    {
        Task<Abono> CreateAbono(Abono abono);
        Task<bool> DeleteAbono(int abonoId);
    }
}
