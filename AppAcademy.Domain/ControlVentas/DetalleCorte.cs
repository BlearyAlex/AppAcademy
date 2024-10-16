﻿namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetalleCorte
    {
        public string DetalleCorteId { get; set; } = Guid.NewGuid().ToString();
        public string? TipoPago { get; set; }
        public decimal Monto { get; set; }

        // Relaciones
        public string? CorteId { get; set; }
        public Corte? Corte { get; set; }

        public string VentaId { get; set; }
        public Venta? Venta { get; set; }
    }
}
