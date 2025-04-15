using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AppAcademy.Infrastucture.Identity
{
    public class IdentityInitializer
    {
        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "User", "Academia", "Ventas" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task InitializeAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string userName = "Academia";
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin123!"; // Asegúrate de que cumpla con los requisitos de password
            string roleName = "Admin";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = userName,
                    Email = adminEmail,
                    EmailConfirmed = false,
                    FullName = "Administrador del sistema"
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    // Opcional: registrar errores
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creando el usuario: {error.Description}");
                    }
                }
            }
        }
    }
}
