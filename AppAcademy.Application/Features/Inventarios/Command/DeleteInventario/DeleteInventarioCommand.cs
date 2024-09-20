using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Command.DeleteInventario
{
    public class DeleteInventarioCommand : IRequest
    {
        public string InventarioId { get; set; }
    }
}
