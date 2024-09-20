using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Salidas.Command.CreateSalida
{
    public class CreateSalidaCommand : IRequest<string>
    {
        public DateTime FechaSalida { get; set; }
        public int TotalProductosSalida { get; set; }
        public string? Comentarios { get; set; }

        // Relaciones
        public string? UserId { get; set; }
    }
}
