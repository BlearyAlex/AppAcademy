using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class ClienteRepository : AsyncRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ClienteTieneVentasActivas(string clienteId)
        {
            return await _dbContext.Ventas.AnyAsync(v => v.ClienteId == clienteId);
        }
    }
}
