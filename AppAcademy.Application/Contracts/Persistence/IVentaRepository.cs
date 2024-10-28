using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IVentaRepository : IAsyncRepository<Venta>
    {
        Task<string> CreateVentaWithProduct(Venta nuevaVenta);
        Task<List<Venta>> GetVentasWithProductos();
    }
}
