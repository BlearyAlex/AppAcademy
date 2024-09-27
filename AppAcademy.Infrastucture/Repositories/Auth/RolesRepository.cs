using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.DTOs;
using AppAcademy.Domain.Auth;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories.Auth
{
    public class RolesRepository : AsyncRepository<Rol>, IRolesRepository
    {
        public RolesRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AssignPermissionsToRole(string rolId, List<string> permissionIds)
        {
            var rol = await _dbContext.Roles.Include(r => r.Permisos).SingleOrDefaultAsync(r => r.RolId == rolId);

            if (rol == null) throw new Exception("Rol no encontrado");

            foreach (var permisoId in permissionIds)
            {
                var permiso = await _dbContext.Permisos.FindAsync(permisoId);
                if(permiso != null && !rol.Permisos.Contains(permiso))
                {
                    rol.Permisos.Add(permiso);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateRolWithPermissions(RolDto rolDto)
        {
            var newRol = new Rol
            {
                NameRol = rolDto.NameRol
            };

            await _dbContext.Roles.AddAsync(newRol);
            await _dbContext.SaveChangesAsync();

            await AssignPermissionsToRole(newRol.RolId, rolDto.Permisos);
        }
    }
}
