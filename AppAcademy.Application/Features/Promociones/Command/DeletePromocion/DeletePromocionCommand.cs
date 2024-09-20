using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Promociones.Command.DeletePromocion
{
    public class DeletePromocionCommand : IRequest
    {
        public string PromocionId { get; set; }
    }
}
