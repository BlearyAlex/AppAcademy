using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IVentaRepository : IAsyncRepository<Venta>
    {
        Task<Venta> CreateVenta(Venta venta);
        Task<bool> UpdateVentaSaldo(Venta venta);
        Task<bool> DeleteVenta(string ventaId);
        Task<Venta> GetVentaById(string ventaId);
    }
}
