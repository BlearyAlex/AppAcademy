using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class ClienteRepository : AsyncRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
