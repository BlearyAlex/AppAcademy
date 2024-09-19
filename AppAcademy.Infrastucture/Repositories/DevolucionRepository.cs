using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Infrastucture.Repositories
{
    public class DevolucionRepository : AsyncRepository<Devolucion>, IDevolucionRepository
    {
        public DevolucionRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
