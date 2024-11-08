using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Estudiantes.Commands.DeleteEstudiante
{
    public class DeleteEstudianteCommand : IRequest
    {
        public string EstudianteId { get; set; }
    }
}
