using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Commands.DeleteCorte
{
    public class DeleteCorteCommand : IRequest
    {
        public string CorteId { get; set; }
    }
}
