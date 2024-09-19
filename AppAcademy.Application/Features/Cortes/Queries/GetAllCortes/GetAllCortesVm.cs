using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Features.Cortes.Queries.GetAllCortes
{
    public class GetAllCortesVm
    {
        public DateTime FechaCorte { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
        public decimal TotalVales { get; set; }
        public decimal TotalDevoluciones { get; set; }
        public string? Comentarios { get; set; }

        public List<DetalleCorte> DetalleCortes { get; set; } = [];
    }
}
