using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Domain.ControlAcademia
{
    public class AbonoAcademy
    {
        public int AbonoAcademyId { get; set; }
        public DateTime FechaAbono { get; set; }
        public decimal Monto { get; set; }

        public int? PaymentId { get; set; }
        public Payment Payment { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}
