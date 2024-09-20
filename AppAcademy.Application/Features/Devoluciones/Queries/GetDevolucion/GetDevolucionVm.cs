using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Queries.GetDevolucion
{
    public class GetDevolucionVm
    {
        public decimal Cantidad { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaDevolucion { get; set; }
    }
}
