using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.Filters;
using AppAcademy.Infrastucture.BackgroundServices;
using AppAcademy.Infrastucture.Persistence;
using AppAcademy.Infrastucture.Repositories;
using AppAcademy.Infrastucture.Repositories.Auth;
using AppAcademy.Infrastucture.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppAcademy.Infrastucture
{
    public static class InfrastuctureServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppAcademyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            // Configurar la autenticación JWT
            var key = configuration.GetValue<string>("Jwt:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // Deshabilitar tolerancia en la expiración (por defecto es de 5 minutos)
                    };
                });

            #region PermissionHandler
            services.AddAuthorization(config =>
            {
                config.AddPolicy("ManageProducts", policy =>
                policy.Requirements.Add(new PermissionAttribute("ManageProducts")));
            });


            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            #endregion

            #region RefreshTokenBackgroundService
            // Configurar servicios en segundo plano
            services.AddHostedService<RefreshTokenCleanupService>();
            #endregion

            #region Auth
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            #endregion

            #region ControlVentas
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
            #endregion

            #region SeedData
            // Agregar SeedData para inicializar datos
            services.AddTransient<SeedData>();
            #endregion

            return services;
        }
    }
}
