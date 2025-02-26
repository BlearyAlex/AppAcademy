using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Permissions.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, GetPermissionVm>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<GetPermissionVm> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdInt(request._PermissionId);

            return _mapper.Map<GetPermissionVm>(permission);
        }
    }
}
