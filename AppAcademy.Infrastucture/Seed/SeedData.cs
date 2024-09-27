using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace AppAcademy.Infrastucture.Seed
{
    public class SeedData
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedData(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Initialize()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppAcademyDbContext>(); 
                                                                                                 
                if (!await dbContext.Permisos.AnyAsync()) 
                {
                    var permisos = PredefinedPermissions.GetDefaultPermissions();
                    await dbContext.Permisos.AddRangeAsync(permisos);
                    await dbContext.SaveChangesAsync();
                }

                // Sembrar roles
                if (!await dbContext.Roles.AnyAsync()) 
                {
                    var roles = PredefinedRoles.GetDefaultRoles();
                    await dbContext.Roles.AddRangeAsync(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
