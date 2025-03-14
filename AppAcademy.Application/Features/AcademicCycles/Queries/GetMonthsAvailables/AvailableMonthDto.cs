using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetMonthsAvailables
{
    public class AvailableMonthDto
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string Label { get; set; } // Por ejemplo, "Enero", "Febrero", etc.
    }
}
