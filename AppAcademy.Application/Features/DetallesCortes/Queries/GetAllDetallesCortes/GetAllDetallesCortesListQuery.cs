﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes
{
    public class GetAllDetallesCortesListQuery : IRequest<List<GetAllDetallesCortesVm>>
    {
    }
}
