using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Salidas.Command.DeleteSalida
{
    public class DeleteSalidaCommand : IRequest
    {
        public string SalidaId { get; set; }
    }
}
