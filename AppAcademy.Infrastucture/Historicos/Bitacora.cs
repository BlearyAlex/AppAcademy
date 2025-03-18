using AppAcademy.Infrastucture.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Infrastucture.Historicos
{
    public class Bitacora
    {
        public string BitacoraId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }
        public string UsuarioId { get; set; }
        public AppUser Usuario { get; set; }

        public string Descripcion { get; set; }
        public string? ReferenciaId { get; set; } // ID del estudiante, pago o abono
        public string TipoReferencia { get; set; } // "Student", "Payment", "AbonoAcademy"
    }
}
