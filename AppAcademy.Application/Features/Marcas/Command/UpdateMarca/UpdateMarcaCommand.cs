using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Command.UpdateMarca
{
    public class UpdateMarcaCommand : IRequest
    {
        public string MarcaId { get; set; } 
        public string? Nombre { get; set; }
    }
}
