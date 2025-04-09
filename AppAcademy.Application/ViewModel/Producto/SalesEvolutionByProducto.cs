using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.ViewModel.Producto
{
    public class SalesEvolutionByProducto
    {
        public string Producto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Color { get; set; }
    }
}
