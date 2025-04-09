using AppAcademy.Application.DTOs.Venta;
using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IVentaRepository : IAsyncRepository<Venta>
    {
        Task<Venta> CreateVenta(Venta venta, string userName);
        Task<bool> UpdateVentaSaldo(Venta venta);
        Task<bool> DeleteVenta(string ventaId, string userName);
        Task<GetVentaVm> GetVentaById(string ventaId);
        Task<List<GetAllVentasVm>> GetAllVentas();

        #region DirectMethods
        Task<List<TopSellingProducts>> TopSellingProducts(DateTime startDate, DateTime endDate);
        Task<List<SalesByRank>> GetSalesByDateRange(DateTime startDate, DateTime endDate);
        Task<List<SalesPerWeekViewModel>> GetSalesPerWeek(DateTime currentDate);
        Task<List<SalesPerMonthViewModel>> GetSalesPerMonth();
        #endregion
    }
}
