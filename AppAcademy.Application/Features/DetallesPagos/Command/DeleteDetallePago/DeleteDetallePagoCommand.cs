﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesPagos.Command.DeleteDetallePago
{
    public class DeleteDetallePagoCommand : IRequest
    {
        public string DetallePagoId { get; set; }
    }
}
