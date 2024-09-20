using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Queries.GetEntrada
{
    public class GetEntradaQuery : IRequest<GetEntradaVm>
    {
        public string _EntradaId { get; set; }

        public GetEntradaQuery(string entradaId)
        {
            _EntradaId = entradaId;
        }
    }
}
