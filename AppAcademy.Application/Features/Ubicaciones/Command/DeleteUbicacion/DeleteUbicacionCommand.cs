using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Command.DeleteUbicacion
{
    public class DeleteUbicacionCommand : IRequest
    {
        public string UbicacionId { get; set; }
    }
}
