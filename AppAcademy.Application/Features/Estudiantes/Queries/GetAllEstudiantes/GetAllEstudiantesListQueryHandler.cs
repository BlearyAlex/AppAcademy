using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetAllEstudiantes
{
    public class GetAllEstudiantesListQueryHandler : IRequestHandler<GetAllEstudiantesListQuery, List<GetAllEstudiantesVm>>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public GetAllEstudiantesListQueryHandler(IEstudianteRepository estudianteRepository, IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllEstudiantesVm>> Handle(GetAllEstudiantesListQuery request, CancellationToken cancellationToken)
        {
            var studentsList = await _estudianteRepository.GetAllAsync();

            return _mapper.Map<List<GetAllEstudiantesVm>>(studentsList);
        }
    }
}
