using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;

namespace AppAcademy.Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Commands
            CreateMap<CreateProductoCommand, Producto>();
            CreateMap<UpdateProductoCommand, Producto>();

            // Queries
            CreateMap<Producto, GetAllProductosVm>()
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(src => src.EstadoProducto.ToString()));

            CreateMap<Producto, GetProductsByCategoriaVm>();
            CreateMap<Producto, GetProductByIdVm>();
        }
    }
}
