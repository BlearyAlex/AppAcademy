using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IReciboAbonoPdfService
    {
        byte[] GenerarReciboAbonoPDF(Venta venta, Abono abono);
    }
}
