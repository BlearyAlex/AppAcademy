using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Bitacora.Queries.GetBitacoras
{
    public class GetBitacorasListQuery : IRequest<List<GetBitacorasVm>>
    {
    }
}
