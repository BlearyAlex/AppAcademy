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

        #region AssignPermissionsToRole
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
        #endregion

        #region CreateRolWithPermissions
        public async Task CreateRolWithPermissions(RolDto rolDto)
        {
            var newRol = new Rol
            {
                NameRol = rolDto.NameRol,
                Permisos = new List<Permiso>()
            };

            await _dbContext.Roles.AddAsync(newRol);
            await _dbContext.SaveChangesAsync();

            // Asignar permisos solo si la lista no es nula o vacía
            if (rolDto.Permisos != null && rolDto.Permisos.Count > 0)
            {
                await AssignPermissionsToRole(newRol.RolId, rolDto.Permisos);
            }
        }
        #endregion

        #region UpdateRolePermissions
        public async Task UpdateRolePermissions(string rolId, List<string> newPermissionIds)
        {
            var rol = await _dbContext.Roles.Include(r => r.Permisos).SingleOrDefaultAsync(r => r.RolId == rolId);

            if (rol == null) throw new Exception("Rol no encontrado");

            // Remover permisos que ya no estan en la lista
            rol.Permisos.RemoveAll(p => !newPermissionIds.Contains(p.PermisoId));

            // Agregar nuevos permisos que no estan en la lista actual
            foreach (var permisoId in newPermissionIds)
            {
                if(!rol.Permisos.Any(p => p.PermisoId == permisoId))
                {
                    var permiso = await _dbContext.Permisos.FindAsync(permisoId);
                    if (permiso != null)
                    {
                        rol.Permisos.Add(permiso);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region DeleteRolWithPermissions
        public async Task DeleteRolWithPermissions(string rolId)
        {
            var rol = await _dbContext.Roles.Include(r => r.Permisos).SingleOrDefaultAsync(r => r.RolId == rolId);

            if (rol == null) throw new Exception("Rol no encontrado");

            // Remover todos los permisos asociados al rol
            rol.Permisos.Clear();

            // Eliminar el rol
            _dbContext.Roles.Remove(rol);

            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
