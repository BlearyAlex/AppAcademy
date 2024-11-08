using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteById
{
    public class GetEstudianteByIdQuery : IRequest<GetEstudianteByIdVm>
    {
        public string EstudianteId { get; set; }

        public GetEstudianteByIdQuery(string estudianteId)
        {
            EstudianteId = estudianteId;
        }
    }
}
