using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Promociones.Queries.GetAllPromociones
{
    public class GetAllPromocionesListQuery : IRequest<List<GetAllPromocionesVm>>
    {
    }
}
