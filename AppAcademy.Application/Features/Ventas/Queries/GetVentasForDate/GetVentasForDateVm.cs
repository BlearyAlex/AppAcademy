namespace AppAcademy.Application.Features.Ventas.Queries.GetVentasForDate
{
    public class GetVentasForDateVm
    {
        public string Fecha { get; set; } // Puede ser "Día", "Semana" o "Mes/Año"
        public decimal TotalVentas { get; set; }
        public int TotalProductos { get; set; }
    }
}
