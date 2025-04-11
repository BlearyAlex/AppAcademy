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
        Task<int> CreateAbono(string ventaId, decimal montoAbonado, string userName);
        Task<bool> DeleteAbono(int abonoId, string userName);
        Task<Abono> GeneratePdf(int abonoId);
    }
}
