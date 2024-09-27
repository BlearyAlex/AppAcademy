using AppAcademy.Domain.Auth;
namespace AppAcademy.Infrastucture.Seed
{
    public static class PredefinedPermissions
    {
        public static List<Permiso> GetDefaultPermissions()
        {
            return new List<Permiso>
            {
                new Permiso { NamePermiso = "ManageProducts", Description = "Permite utilizar el modulo de Productos" },
                new Permiso { NamePermiso = "ManageCategorias", Description = "Permite utilizar el modulo de Categorias" },
                new Permiso { NamePermiso = "ManageProveedores", Description = "Permite utilizar el modulo de Proveedores" },
            };
        }
    }
}
