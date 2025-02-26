using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.StudentPermissions.Commands.UpdateStudentPermission
{
    public class UpdateStudentPermissionCommandHandler : IRequestHandler<UpdateStudentPermissionCommand>
    {
        private readonly IStudentPermissionRepository _studentPermissionRepository;
        private readonly ILogger<UpdateStudentPermissionCommandHandler> _logger;

        public UpdateStudentPermissionCommandHandler(IStudentPermissionRepository studentPermissionRepository, ILogger<UpdateStudentPermissionCommandHandler> logger)
        {
            _studentPermissionRepository = studentPermissionRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateStudentPermissionCommand request, CancellationToken cancellationToken)
        {
            var findStudenPermission = await _studentPermissionRepository.GetByIdInt(request.StudentPermissionId);

            if (findStudenPermission == null)
            {
                _logger.LogError($"No se encontro el id del career {request.StudentPermissionId}");
                throw new NotFoundException(nameof(StudentPermission), request.StudentPermissionId);
            }

            findStudenPermission.FechaInicio = request.FechaInicio;
            findStudenPermission.FechaFin = request.FechaFin;
            findStudenPermission.StudentId = request.StudentId;
            findStudenPermission.PermissionId = request.PermissionId;

            await _studentPermissionRepository.UpdateAsync(findStudenPermission);

            _logger.LogInformation($"La operacion fue exitosa {request.StudentPermissionId}");
        }
    }
}
