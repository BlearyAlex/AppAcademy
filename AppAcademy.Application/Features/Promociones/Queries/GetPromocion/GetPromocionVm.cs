using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Features.Promociones.Queries.GetPromocion
{
    public class GetPromocionVm
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Descuento { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
