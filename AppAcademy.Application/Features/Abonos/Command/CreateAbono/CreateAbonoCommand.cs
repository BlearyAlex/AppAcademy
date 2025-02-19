using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Abonos.Command.CreateAbono
{
    public class CreateAbonoCommand : IRequest<string>
    {
        public string VentaId { get; set; }
        public decimal MontoAbonado { get; set; }
    }
}
