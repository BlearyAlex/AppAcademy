using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Command.DeleteMarca
{
    public class DeleteMarcaCommand : IRequest
    {
        public string MarcaId { get; set; }
    }
}
