using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository, ILogger<DeleteStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            return await _studentRepository.DeleteStudent(request, request.UserName);
        }
    }
}
