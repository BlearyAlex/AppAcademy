using AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle;
using AppAcademy.Application.Features.Bitacora.Queries.GetBitacoras;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareers;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using AppAcademy.Application.Features.Clientes.Commands.CreateCliente;
using AppAcademy.Application.Features.Clientes.Commands.UpdateCliente;
using AppAcademy.Application.Features.Clientes.Queries.GetAllCliente;
using AppAcademy.Application.Features.Clientes.Queries.GetClienteById;
using AppAcademy.Application.Features.Cortes.Commands.CreateCorte;
using AppAcademy.Application.Features.Cortes.Commands.UpdateCorte;
using AppAcademy.Application.Features.Cortes.Queries.GetAllCortes;
using AppAcademy.Application.Features.Cortes.Queries.GetCorte;
using AppAcademy.Application.Features.Entradas.Commands.CreateEntrada;
using AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada;
using AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas;
using AppAcademy.Application.Features.EntradasProductos.Command.CreateEntrada;
using AppAcademy.Application.Features.EntradasProductos.Command.UpdateEntrada;
using AppAcademy.Application.Features.EntradasProductos.Queries.GetAllEntradas;
using AppAcademy.Application.Features.EntradasProductos.Queries.GetEntrada;
using AppAcademy.Application.Features.Inventarios.Command.CreateInventario;
using AppAcademy.Application.Features.Inventarios.Command.UpdateInventario;
using AppAcademy.Application.Features.Inventarios.Queries.GetAllInventarios;
using AppAcademy.Application.Features.Inventarios.Queries.GetInventario;
using AppAcademy.Application.Features.Marcas.Command.CreateMarca;
using AppAcademy.Application.Features.Marcas.Command.UpdateMarca;
using AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas;
using AppAcademy.Application.Features.Marcas.Queries.GetMarca;
using AppAcademy.Application.Features.Permissions.Queries.GetAllPermissions;
using AppAcademy.Application.Features.Permissions.Queries.GetPermission;
using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor;
using AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetAllProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetProveedorById;
using AppAcademy.Application.Features.Salidas.Command.CreateSalida;
using AppAcademy.Application.Features.Salidas.Command.UpdateSalida;
using AppAcademy.Application.Features.Salidas.Queries.GetAllSalidas;
using AppAcademy.Application.Features.Salidas.Queries.GetSalida;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using AppAcademy.Application.Features.Ventas.Command.CreateVenta;
using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Logs;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;

namespace AppAcademy.Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Productos
            // Commands 
            CreateMap<CreateProductoCommand, Producto>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.MarcaId))
                .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.ProveedorId));
            CreateMap<UpdateProductoCommand, Producto>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.MarcaId))
                .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.ProveedorId));

            // Queries
            CreateMap<Producto, GetAllProductosVm>()
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(src => src.EstadoProducto.ToString()));
            CreateMap<Producto, GetProductsByCategoriaVm>()
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(src => src.EstadoProducto.ToString()));
            CreateMap<Producto, GetProductByIdVm>()
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(src => src.EstadoProducto.ToString()));
            CreateMap<Producto, GetProductsByNameVm>()
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(src => src.EstadoProducto.ToString()));
            #endregion

            #region Categorias
            // Commands
            CreateMap<CreateCategoriaCommand, Categoria>();
            CreateMap<UpdateCategoriaCommand, Categoria>();

            // Queries
            CreateMap<Categoria, GetAllCategoriasVm>();
            CreateMap<Categoria, GetCategoriaByIdVm>();
            #endregion

            #region Proveedores

            // Commands
            CreateMap<CreateProveedorCommand, Proveedor>();
            CreateMap<UpdateProveedorCommand, Proveedor>();

            // Queries
            CreateMap<Proveedor, GetAllProveedoresVm>();
            CreateMap<Proveedor, GetProveedorByIdVm>();
            #endregion

            #region Clientes

            // Commands
            CreateMap<CreateClienteCommand, Cliente>();
            CreateMap<UpdateClienteCommand, Cliente>();

            // Queries
            CreateMap<Cliente, GetAllClientesVm>();
            CreateMap<Cliente, GetClienteVm>();
            #endregion

            #region Cortes
            // Commands
            CreateMap<CreateCorteCommand, Corte>();
            CreateMap<UpdateCorteCommand, Corte>();

            // Queries
            CreateMap<Corte, GetAllCortesVm>();
            CreateMap<Corte, GetCorteVm>();
            #endregion

            #region DetalleCorte
            //CreateMap<CreateDetalleCorteCommand, DetalleCorte>()
            //    .ForMember(dest => dest.CorteId, opt => opt.MapFrom(src => src.CorteId))
            //    .ForMember(dest => dest.VentaId, opt => opt.MapFrom(src => src.VentaId));
            //CreateMap<UpdateDetalleCorteCommand, DetalleCorte>()
            //     .ForMember(dest => dest.CorteId, opt => opt.MapFrom(src => src.CorteId))
            //     .ForMember(dest => dest.VentaId, opt => opt.MapFrom(src => src.VentaId));

            //CreateMap<DetalleCorte, GetAllDetallesCortesVm>();
            //CreateMap<DetalleCorte, GetDetalleCorteVm>();
            #endregion

            #region Entradas
            CreateMap<CreateEntradaCommand, Entrada>();
            //.ForMember(dest => dest.OrigenId, opt => opt.MapFrom(src => src.OrigenId));
            CreateMap<UpdateEntradaCommand, Entrada>();
                //.ForMember(dest => dest.OrigenId, opt => opt.MapFrom(src => src.OrigenId));

            CreateMap<Entrada, GetAllEntradasVm>()
                .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.EntradaProductos));
            CreateMap<Entrada, GetEntradaProductoVm>();
            #endregion

            #region EntradasProductos
            CreateMap<CreateEntradaProductoCommand, EntradaProducto>()
                .ForMember(dest => dest.EntradaId, opt => opt.MapFrom(src => src.EntradaId))
                .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.ProductoId));
            CreateMap<UpdateEntradaProductoCommand, EntradaProducto>()
                .ForMember(dest => dest.EntradaId, opt => opt.MapFrom(src => src.EntradaId))
                .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.ProductoId));

            CreateMap<EntradaProducto, GetAllEntradasProductosVm>();
            CreateMap<EntradaProducto, GetEntradaProductoVm>();
            #endregion

            #region Inventarios
            CreateMap<CreateInventarioCommand, Inventario>()
                .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.ProductoId));
            CreateMap<UpdateInventarioCommand, Inventario>()
                .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.ProductoId));

            CreateMap<Inventario, GetAllInventariosVm>();
            CreateMap<Inventario, GetInventarioVm>();
            #endregion

            #region Marcas
            CreateMap<CreateMarcaCommand, Marca>();
            CreateMap<UpdateMarcaCommand, Marca>();

            CreateMap<Marca, GetAllMarcasVm>();
            CreateMap<Marca, GetMarcaVm>();
            #endregion

            #region Salidas
            CreateMap<CreateSalidaCommand, Salida>();
            CreateMap<UpdateSalidaCommand, Salida>();

            CreateMap<Salida, GetAllSalidasVm>();
            CreateMap<Salida, GetSalidaVm>();
            #endregion

            #region Ventas
            CreateMap<CreateVentaCommand, Venta>()
                //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));

            CreateMap<Venta, GetAllVentasVm>()
                .ForMember(dest => dest.EstadoVenta, opt => opt.MapFrom(src => src.EstadoVenta.ToString()));
            CreateMap<Venta, GetVentaVm>()
                .ForMember(dest => dest.EstadoVenta, opt => opt.MapFrom(src => src.EstadoVenta.ToString()));
            #endregion

            #region Students
            CreateMap<Student, GetAllStudentsVm>()
                .ForMember(dest => dest.EstadoEstudiante, opt => opt.MapFrom(src => src.EstadoEstudiante.ToString()));
            CreateMap<Student, GetStudentVm>()
                 .ForMember(dest => dest.EstadoEstudiante, opt => opt.MapFrom(src => src.EstadoEstudiante.ToString()))
                 .ForMember(dest => dest.EstadoEstudiante, opt => opt.MapFrom(src => src.EstadoEstudiante.ToString()));
            #endregion

            #region Career
            CreateMap<Career, GetAllCareersVm>()
                .ForMember(dest => dest.Activa, opt => opt.MapFrom(src => src.Activa.ToString()));
            CreateMap<Career, GetCareerVm>()
                .ForMember(dest => dest.Activa, opt => opt.MapFrom(src => src.Activa.ToString()));
            #endregion

            #region AcademicCycle
            CreateMap<AcademicCycle, GetAllCyclesVm>();
            CreateMap<AcademicCycle, GetCycleVm>();
            #endregion

            #region Payment
           
            #endregion

            #region Permission
            CreateMap<Permission, GetAllPermissionsVm>();
            CreateMap<Permission, GetPermissionVm>();
            #endregion

            #region Bitacora
            CreateMap<Bitacora, GetBitacorasVm>();
            #endregion
        }
    }
}
