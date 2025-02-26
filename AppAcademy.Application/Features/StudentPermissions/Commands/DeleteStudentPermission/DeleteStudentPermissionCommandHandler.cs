using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.StudentPermissions.Commands.DeleteStudentPermission
{
    public class DeleteStudentPermissionCommandHandler : IRequestHandler<DeleteStudentPermissionCommand>
    {
        private readonly IStudentPermissionRepository _studentPermissionRepository;
        private readonly ILogger<DeleteStudentPermissionCommand> _logger;

        public DeleteStudentPermissionCommandHandler(IStudentPermissionRepository studentPermissionRepository, ILogger<DeleteStudentPermissionCommand> logger)
        {
            _studentPermissionRepository = studentPermissionRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteStudentPermissionCommand request, CancellationToken cancellationToken)
        {
            var findStudentPermission = await _studentPermissionRepository.GetByIdInt(request.StudentPermissionId);
            if (findStudentPermission == null)
            {
                _logger.LogError($"{request.StudentPermissionId} StudentPermission no existe en el sistema");
                throw new NotFoundException(nameof(findStudentPermission), request.StudentPermissionId);
            }

            await _studentPermissionRepository.DeleteAsync(findStudentPermission);

            return;
        }
    }
}
