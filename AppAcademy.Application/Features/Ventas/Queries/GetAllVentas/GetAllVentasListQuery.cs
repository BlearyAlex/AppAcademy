﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasListQuery : IRequest<List<GetAllVentasVm>>
    {
    }
}
