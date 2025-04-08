using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.DTOs.Venta
{
    public class SalesPerMonthViewModel
    {
        public string MonthName { get; set; } = "";
        public decimal TotalSales { get; set; }
        public int SalesCount { get; set; }
    }
}
