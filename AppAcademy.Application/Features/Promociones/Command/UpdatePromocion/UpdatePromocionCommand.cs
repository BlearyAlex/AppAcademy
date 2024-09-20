using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Promociones.Command.UpdatePromocion
{
    public class UpdatePromocionCommand : IRequest
    {
        public string PromocionId { get; set; } 
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Descuento { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
