using AppAcademy.Domain.PuntoDeVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface ISalidaRepository : IAsyncRepository<Salida>
    {
    }
}
