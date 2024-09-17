﻿namespace AppAcademy.Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosVm
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal DescuentoBase { get; set; }
        public int Impuesto { get; set; }
        public string EstadoProducto { get; set; } 
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
    }
}
