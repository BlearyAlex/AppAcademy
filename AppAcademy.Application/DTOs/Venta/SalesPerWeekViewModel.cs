using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.DTOs.Venta
{
    public class SalesPerWeekViewModel
    {
        public DateTime WeekStart { get; set; } 
        public decimal TotalSales { get; set; }
        public int SalesCount { get; set; }
    }
}
