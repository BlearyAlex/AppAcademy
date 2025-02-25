using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Commands.CreateStudent
{
    public class CreateCareerCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CreateCareerCommandHandler> _logger;

        public CreateCareerCommandHandler(IStudentRepository studentRepository, ILogger<CreateCareerCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                Email = request.Email,
                Direccion = request.Direccion,
                ImageUrl = request.ImageUrl,
                FechaIngreso = DateTime.Now,
                EstadoEstudiante = request.EstadoEstudiante,
                CareerId = request.CareerId,
            };

            var newStudent = await _studentRepository.AddAsync(student);

            return newStudent.StudentId;
        }
    }
}
