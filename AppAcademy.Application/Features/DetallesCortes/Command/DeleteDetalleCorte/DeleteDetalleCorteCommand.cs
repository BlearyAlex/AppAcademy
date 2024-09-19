using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Command.DeleteDetalleCorte
{
    public class DeleteDetalleCorteCommand : IRequest
    {
        public string DetalleCorteId { get; set; }
    }
}
