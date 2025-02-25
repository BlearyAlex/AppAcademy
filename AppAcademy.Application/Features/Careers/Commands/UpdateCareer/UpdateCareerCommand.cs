using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Careers.Commands.UpdateCareer
{
    public class UpdateCareerCommand : IRequest
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public int DuracionSemestres { get; set; }
        public decimal CostoMensual { get; set; }
        public bool Activa { get; set; }
    }
}
