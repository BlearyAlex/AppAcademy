using AppAcademy.Domain.Auth;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.PuntoDeVenta;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Persistence
{
    public class AppAcademyDbContext : DbContext
    {
        public AppAcademyDbContext(DbContextOptions<AppAcademyDbContext> options) : base(options)
        {
        }

        // Auth
        public DbSet<EstadoUser> EstadoUsers { get; set; }
        public DbSet<HistorialAcceso> HistorialAccesos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Rol> Roles {  get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        // Punto Venta
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Corte> Cortes { get; set; }
        public DbSet<DetalleCorte> DetalleCortes { get; set; }
        public DbSet<DetallePago> DetallePagos { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<EntradaProducto> EntradaProductos { get; set; }
        public DbSet<HistorialInventario> HistorialInventarios { get; set; }
        public DbSet<HistorialPrecio> HistorialPrecios { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        // Control Academia
        public DbSet<Colegiatura> Colegiaturas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set;}
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MaterialAdeudo> MaterialAdeudos { get; set; }
        public DbSet<Materia_Estudiante> MateriaEstudiantes { get; set; }
    }
}
