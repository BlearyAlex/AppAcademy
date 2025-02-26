using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsListQueryHandler : IRequestHandler<GetAllPermissionsListQuery, List<GetAllPermissionsVm>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public GetAllPermissionsListQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPermissionsVm>> Handle(GetAllPermissionsListQuery request, CancellationToken cancellationToken)
        {
            var permissionsList = await _permissionRepository.GetAllAsync();

            return _mapper.Map<List<GetAllPermissionsVm>>(permissionsList);
        }
    }
}
