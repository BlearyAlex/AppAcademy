using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IReciboAbonoPdfService
    {
        byte[] GenerarReciboAbonoPDF(Venta venta, Abono abono, decimal cambio);
        byte[] GenerarReciboAbonoAcademyPDF(Payment payment, AbonoAcademy abono, decimal cambio);
    }
}
