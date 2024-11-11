using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Commands.DeleteColegiatura
{
    public class DeleteColegiaturaCommand : IRequest
    {
        public string ColegiaturaId { get; set; }
    }
}
