using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.HistorialInventarios.Command.DeleteHistorialInventario
{
    public class DeleteHistorialInventarioCommand : IRequest
    {
        public string HistorialInventarioId { get; set; }
    }
}
