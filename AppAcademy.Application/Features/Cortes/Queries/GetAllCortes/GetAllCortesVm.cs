﻿using AppAcademy.Domain.Auth;
using AppAcademy.Domain.PuntoDeVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Queries.GetAllCortes
{
    public class GetAllCortesVm
    {
        public DateTime FechaCorte { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
        public decimal TotalVales { get; set; }
        public decimal TotalDevoluciones { get; set; }
        public string? Comentarios { get; set; }

        // Relaciones
        public string? UserId { get; set; }
        public User? User { get; set; }

        public List<DetalleCorte> DetalleCortes { get; set; } = [];
    }
}
