using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
using AppAcademy.Domain.PuntoDeVenta;
using System.Threading.Tasks;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IVentaRepository : IAsyncRepository<Venta>
    {
        Task<Venta> CreateVenta(Venta venta);
        Task<bool> UpdateVentaSaldo(Venta venta);
        Task<bool> DeleteVenta(string ventaId);
        Task<GetVentaVm> GetVentaById(string ventaId);
        Task<List<GetAllVentasVm>> GetAllVentas();
    }
}
