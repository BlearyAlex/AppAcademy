using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Infrastucture.Persistence;
using AppAcademy.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppAcademy.Infrastucture
{
    public static class InfrastuctureServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppAcademyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICorteRepository, CorteRepository>();
            services.AddScoped<IDetalleCorteRepository, DetalleCorteRepository>();
            services.AddScoped<IDetallePagoRepository, DetallePagoRepository>();
            services.AddScoped<IDevolucionRepository, DevolucionRepository>();
            services.AddScoped<IEntradaProductoRepository, EntradaProductoRepository>();
            services.AddScoped<IEntradaRepository, EntradaRepository>();
            services.AddScoped<IHistorialInventarioRepository, HistorialInventarioRepository>();
            services.AddScoped<IInventarioRepository, InventarioRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IPromocionRepository, PromocionRepository>();
            services.AddScoped<ISalidaRepository, SalidaRepository>();
            services.AddScoped<IUbicacionRepository, UbicacionRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();

            return services;
        }
    }
}
