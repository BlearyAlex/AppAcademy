using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetAllEstudiantes
{
    public class GetAllEstudiantesListQuery : IRequest<List<GetAllEstudiantesVm>>
    {
    }
}
