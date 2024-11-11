using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class ColegiaturaRepository : AsyncRepository<Colegiatura>, IColegiaturaRepository
    {
        public ColegiaturaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
