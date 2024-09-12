using AppAcademy.Application.Features.Productos.Commands.CreateProducto;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
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

            // Queries
            CreateMap<Producto, GetAllProductosVm>();
        }
    }
}
