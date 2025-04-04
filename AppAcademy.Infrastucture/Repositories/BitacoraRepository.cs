using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.Logs;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class BitacoraRepository : AsyncRepository<Bitacora>, IBitacoraRepository
    {
        public BitacoraRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Bitacora>> GetAllBitacoras()
        {
            var bitacoras = await _dbContext.Bitacora
                .OrderByDescending(b => b.Fecha)
                .ToListAsync();

            return bitacoras;
        }
    }
}
