using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class EntradaRepository : AsyncRepository<Entrada>, IEntradaRepository
    {
        public EntradaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
