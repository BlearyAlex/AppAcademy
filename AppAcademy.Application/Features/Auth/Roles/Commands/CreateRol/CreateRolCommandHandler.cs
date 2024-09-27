using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.DTOs;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.CreateRol
{
    public class CreateRolCommandHandler : IRequestHandler<CreateRolCommand>
    {
        private readonly IRolesRepository _rolesRepository;

        public CreateRolCommandHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task Handle(CreateRolCommand request, CancellationToken cancellationToken)
        {
            var rolDto = new RolDto
            {
                NameRol = request.NameRol,
                Permisos = request.Permisos,
            };

            await _rolesRepository.CreateRolWithPermissions(rolDto);
        }
    }
}
