﻿using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Command.CreateInventario
{
    public class CreateInventarioCommand : IRequest<string>
    {
        public int Cantidad { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Lote { get; set; }
        public DateTime FechaExpiracion { get; set; }

        // Relaciones
        public string? ProductoId { get; set; }
    }
}
