using AppAcademy.Domain.PuntoDeVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas
{
    public class GetAllMarcasVm
    {
        public string? Nombre { get; set; }
        public string? MarcaId { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
