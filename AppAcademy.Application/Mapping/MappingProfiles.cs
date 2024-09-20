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
using AppAcademy.Application.Features.DetallesCortes.Command.CreateDetalleCorte;
using AppAcademy.Application.Features.DetallesCortes.Command.UpdateDetalleCorte;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetDetalleCorte;
using AppAcademy.Application.Features.DetallesPagos.Command.CreateDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Command.UpdateDetallePago;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos;
using AppAcademy.Application.Features.DetallesPagos.Queries.GetDetallePago;
using AppAcademy.Application.Features.Devoluciones.Command.CreateDevolucion;
using AppAcademy.Application.Features.Devoluciones.Command.UpdateDevolucion;
using AppAcademy.Application.Features.Devoluciones.Queries.GetAllDevoluciones;
using AppAcademy.Application.Features.Devoluciones.Queries.GetDevolucion;
using AppAcademy.Application.Features.Entradas.Command.CreateEntrada;
using AppAcademy.Application.Features.Entradas.Command.UpdateEntrada;
using AppAcademy.Application.Features.Entradas.Queries.GetAllEntradas;
using AppAcademy.Application.Features.EntradasProductos.Command.UpdateEntrada;
using AppAcademy.Application.Features.EntradasProductos.Queries.GetAllEntradas;
using AppAcademy.Application.Features.EntradasProductos.Queries.GetEntrada;
using AppAcademy.Application.Features.HistorialInventarios.Command.CreateHistorialInventario;
using AppAcademy.Application.Features.HistorialInventarios.Command.UpdateHistorialInventario;
using AppAcademy.Application.Features.HistorialInventarios.Queries.GetAllHistorialInventario;
using AppAcademy.Application.Features.HistorialInventarios.Queries.GetHistorialInventario;
using AppAcademy.Application.Features.Inventarios.Command.CreateInventario;
using AppAcademy.Application.Features.Inventarios.Command.UpdateInventario;
using AppAcademy.Application.Features.Inventarios.Queries.GetAllInventarios;
using AppAcademy.Application.Features.Inventarios.Queries.GetInventario;
using AppAcademy.Application.Features.Marcas.Command.CreateMarca;
using AppAcademy.Application.Features.Marcas.Command.UpdateMarca;
using AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas;
using AppAcademy.Application.Features.Marcas.Queries.GetMarca;
using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Application.Features.Promociones.Command.CreatePromocion;
using AppAcademy.Application.Features.Promociones.Command.UpdatePromocion;
using AppAcademy.Application.Features.Promociones.Queries.GetAllPromociones;
using AppAcademy.Application.Features.Promociones.Queries.GetPromocion;
using AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor;
using AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetAllProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetProveedorById;
using AppAcademy.Application.Features.Salidas.Command.CreateSalida;
using AppAcademy.Application.Features.Salidas.Command.UpdateSalida;
using AppAcademy.Application.Features.Salidas.Queries.GetAllSalidas;
using AppAcademy.Application.Features.Salidas.Queries.GetSalida;
using AppAcademy.Application.Features.Ubicaciones.Command.CreateUbicacion;
using AppAcademy.Application.Features.Ubicaciones.Command.UpdateUbicacion;
using AppAcademy.Application.Features.Ubicaciones.Queries.GetAllUbicaciones;
using AppAcademy.Application.Features.Ubicaciones.Queries.GetUbicacion;
using AppAcademy.Application.Features.Ventas.Command.CreateVenta;
using AppAcademy.Application.Features.Ventas.Command.UpdateVenta;
using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
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
                .ForMember(dest => dest.EstadoProducto, opt => opt
                .MapFrom(src => src.EstadoProducto.ToString()));
            CreateMap<Producto, GetProductsByCategoriaVm>();
            CreateMap<Producto, GetProductByIdVm>();
            CreateMap<Producto, GetProductsByNameVm>();
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
            CreateMap<CreateCorteCommand, Corte>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UpdateCorteCommand, Corte>();

            // Queries
            CreateMap<Corte, GetAllCortesVm>();
            CreateMap<Corte, GetCorteVm>();
            #endregion

            #region DetalleCorte
            CreateMap<CreateDetalleCorteCommand, DetalleCorte>();
            CreateMap<UpdateDetalleCorteCommand, DetalleCorte>();

            CreateMap<DetalleCorte, GetAllDetallesCortesVm>();
            CreateMap<DetalleCorte, GetDetalleCorteVm>();
            #endregion

            #region DetallesPagos
            CreateMap<CreateDetallePagoCommand, DetallePago>();
            CreateMap<UpdateDetallePagoCommand, DetallePago>();

            CreateMap<DetallePago, GetAllDetallesPagosVm>();
            CreateMap<DetallePago, GetDetallePagoVm>();
            #endregion

            #region Devoluciones
            CreateMap<CreateDevolucionCommand, Devolucion>();
            CreateMap<UpdateDevolucionCommand, Devolucion>();

            CreateMap<Devolucion, GetAllDevolucionesVm>();
            CreateMap<Devolucion, GetDevolucionVm>();
            #endregion

            #region Entradas
            CreateMap<CreateEntradaCommand, Entrada>();
            CreateMap<UpdateEntradaCommand, Entrada>();

            CreateMap<Entrada, GetAllEntradasVm>();
            CreateMap<Entrada, GetEntradaProductoVm>();
            #endregion

            #region EntradasProductos
            CreateMap<CreateEntradaCommand, EntradaProducto>();
            CreateMap<UpdateEntradaProductoCommand, EntradaProducto>();

            CreateMap<EntradaProducto, GetAllEntradasProductosVm>();
            CreateMap<EntradaProducto, GetEntradaProductoVm>();
            #endregion

            #region HistorialInventario
            CreateMap<CreateHistorialInventarioCommand, HistorialInventario>();
            CreateMap<UpdateHistorialInventarioCommand, HistorialInventario>();

            CreateMap<HistorialInventario, GetAllHistorialInventarioVm>();
            CreateMap<HistorialInventario, GetHistorialInventarioVm>();
            #endregion

            #region Inventarios
            CreateMap<CreateInventarioCommand, Inventario>();
            CreateMap<UpdateInventarioCommand, Inventario>();
            
            CreateMap<Inventario, GetAllInventariosVm>();
            CreateMap<Inventario, GetInventarioVm>();
            #endregion

            #region Marcas
            CreateMap<CreateMarcaCommand, Marca>();
            CreateMap<UpdateMarcaCommand, Marca>();

            CreateMap<Marca, GetAllMarcasVm>();
            CreateMap<Marca, GetMarcaVm>();
            #endregion

            #region Promociones
            CreateMap<CreatePromocionCommand, Promocion>();
            CreateMap<UpdatePromocionCommand, Promocion>();

            CreateMap<Promocion, GetAllPromocionesVm>();
            CreateMap<Promocion, GetPromocionVm>();
            #endregion

            #region Salidas
            CreateMap<CreateSalidaCommand, Salida>();
            CreateMap<UpdateSalidaCommand, Salida>();

            CreateMap<Salida, GetAllSalidasVm>();
            CreateMap<Salida, GetSalidaVm>();
            #endregion

            #region Ubicaciones
            CreateMap<CreateUbicacionCommand, Ubicacion>();
            CreateMap<UpdateUbicacionCommand, Ubicacion>();

            CreateMap<Ubicacion, GetAllUbicacionesVm>();
            CreateMap<Ubicacion, GetUbicacionVm>();
            #endregion

            #region Ventas
            CreateMap<CreateVentaCommand, Venta>();
            CreateMap<UpdateVentaCommand, Venta>();

            CreateMap<Venta, GetAllVentasVm>();
            CreateMap<Venta, GetVentaVm>();
            #endregion
        }
    }
}
