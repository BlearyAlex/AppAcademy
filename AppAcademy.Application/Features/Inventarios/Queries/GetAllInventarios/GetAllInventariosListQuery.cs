﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Queries.GetAllInventarios
{
    public class GetAllInventariosListQuery : IRequest<List<GetAllInventariosVm>>
    {
    }
}
