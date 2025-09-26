using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using AppAcademy.Infrastucture.Repositories;
using AppAcademy.Infrastucture.Repositories.Auth;
using AppAcademy.Infrastucture.Repositories.ControlAcademia;
using AppAcademy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using System.Security.Claims;
using System.Text;

namespace AppAcademy.Infrastucture
{
    public static class InfrastuctureServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var connectionStringEnv = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__SQLCONNECTION");
            if (!string.IsNullOrEmpty(connectionStringEnv))
            {
                configuration["ConnectionStrings:SqlConnection"] = connectionStringEnv;
            }

            services.AddDbContext<AppAcademyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
                sqlOptions => 
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                ));

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppAcademyDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("P3m7e0NbBTIWUdrv03TNxoHcBvqmXjdOODN7iMoEbeo")),
                        RoleClaimType = ClaimTypes.Role
                    };
                });

            #region ControlVentas
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICorteRepository, CorteRepository>();
            services.AddScoped<IDetalleCorteRepository, DetalleCorteRepository>();
            services.AddScoped<IEntradaProductoRepository, EntradaProductoRepository>();
            services.AddScoped<IEntradaRepository, EntradaRepository>();
            services.AddScoped<IInventarioRepository, InventarioRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<ISalidaRepository, SalidaRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<IAbonoRepository, AbonoRepository>();
            services.AddScoped<IBitacoraRepository, BitacoraRepository>();
            #endregion

            #region ControlAcademia
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<IAcademicCycleRepository, AcademicCycleRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IStudentPaymentStatusRepository, StudentPaymentStatusRepository>();
            services.AddScoped<IStudentPermissionRepository, StudentPermissionRepository>();
            services.AddScoped<IAbonoAcademyRepository, AbonoAcademyRepository>();
            #endregion

            #region Auth
            services.AddScoped<RefreshTokenService>();
            #endregion

            services.AddScoped<IReciboAbonoPdfService, ReciboAbonoPdfService>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            return services;
        }
    }
}
