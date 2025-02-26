using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.StudentPermissions.Commands.CreateStudentPermission
{
    public class CreateStudentPermissionCommandHandler : IRequestHandler<CreateStudentPermissionCommand, int>
    {
        private readonly IStudentPermissionRepository _studentPermissionRepository;
        private readonly ILogger<CreateStudentPermissionCommandHandler> _logger;

        public CreateStudentPermissionCommandHandler(IStudentPermissionRepository studentPermissionRepository, ILogger<CreateStudentPermissionCommandHandler> logger)
        {
            _studentPermissionRepository = studentPermissionRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStudentPermissionCommand request, CancellationToken cancellationToken)
        {
            var studentPermission = new StudentPermission
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                StudentId = request.StudentId,
                PermissionId = request.PermissionId,
            };

            var newStudentPermission = await _studentPermissionRepository.AddAsync(studentPermission);

            return newStudentPermission.StudentPermissionId;
        }
    }
}
