using AppAcademy.Application.Features.Categorias.Commands.CreateCategoria;
using AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById;
using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor;
using AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetAllProveedor;
using AppAcademy.Application.Features.Proveedores.Queries.GetProveedorById;
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
        }
    }
}
