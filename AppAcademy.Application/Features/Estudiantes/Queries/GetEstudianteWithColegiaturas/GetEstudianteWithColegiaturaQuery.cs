using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteWithColegiaturas
{
    public class GetEstudianteWithColegiaturaQuery : IRequest<GetEstudianteWithColegiaturaVm>
    {
        public string EstudianteId { get; set; }

        public GetEstudianteWithColegiaturaQuery(string estudianteId)
        {
            EstudianteId = estudianteId;
        }
    }
}
