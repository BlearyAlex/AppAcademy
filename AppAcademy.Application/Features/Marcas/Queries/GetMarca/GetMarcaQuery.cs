﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Queries.GetMarca
{
    public class GetMarcaQuery : IRequest<GetMarcaVm>
    {
        public string _MarcaId { get; set; }
        
        public GetMarcaQuery(string marcaId)
        {
            _MarcaId = marcaId;
        }


    }
}
