using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Bitacora.Queries.GetBitacoras
{
    public class GetBitacorasVm
    {
        public string BitacoraId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }
        public string UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public string? ReferenciaId { get; set; }
        public string TipoReferencia { get; set; } // "Student", "Payment", "AbonoAcademy"
    }
}
