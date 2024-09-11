﻿namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetalleVenta
    {
        public string DetalleVentaId { get; set; } = Guid.NewGuid().ToString();
        public TipoPago Tipo { get; set; }
        public decimal Monto { get; set; }
        public int DescuentoProducto { get; set; }

        // Enum para los tipos de pago
        public enum TipoPago
        {
            Efectivo,
            TarjetaDeCredito,
            Transferencia,
            Otro
        }

        // Relaciones
        public Venta? Venta { get; set; }
        public Producto? Producto { get; set; }

    }
}
