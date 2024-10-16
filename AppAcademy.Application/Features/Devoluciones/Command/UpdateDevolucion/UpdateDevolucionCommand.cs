﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Command.UpdateDevolucion
{
    public class UpdateDevolucionCommand : IRequest
    {
        public string DevolucionId { get; set; }
        public decimal Cantidad { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        // Relaciones
        public string? VentaId { get; set; }
        public string? ProductoId { get; set; }
        public string? UserId { get; set; }
    }
}
