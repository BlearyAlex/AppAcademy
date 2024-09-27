using AppAcademy.Domain.Auth;

namespace AppAcademy.Infrastucture.Seed
{
    public static class PredefinedRoles
    {
        public static List<Rol> GetDefaultRoles()
        {
            return new List<Rol>
            {
                new Rol { NameRol = "Administrador", Description = "Acceso completo a todas las funcionalidades" },
                new Rol { NameRol = "Vendedor", Description = "Acceso a la gestión de productos y ventas" },
                new Rol { NameRol = "Usuario", Description = "Acceso limitado a funcionalidades básicas" }
            };
        }
    }
}
