using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVentaForMonth
{
    public class GetVentaForMonthVm
    {
        public int Mes { get; set; }
        public int Año { get; set; }
        public decimal TotalVentas { get; set; }
    }
}
