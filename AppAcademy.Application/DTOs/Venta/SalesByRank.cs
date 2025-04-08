using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.DTOs.Venta
{
    public class SalesByRank
    {
        public string VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
