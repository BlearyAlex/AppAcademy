﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Salidas.Queries.GetAllSalidas
{
    public class GetAllSalidasListQuery : IRequest<List<GetAllSalidasVm>>
    {
    }
}
