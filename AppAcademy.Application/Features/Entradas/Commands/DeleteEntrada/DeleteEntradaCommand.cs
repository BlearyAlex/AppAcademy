using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada
{
    public class DeleteEntradaCommand : IRequest
    {
        public string EntradaId { get; set; }
    }
}
