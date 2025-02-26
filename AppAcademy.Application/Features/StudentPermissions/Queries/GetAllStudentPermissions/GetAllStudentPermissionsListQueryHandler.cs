using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.StudentPermissions.Queries.GetAllStudentPermissions
{
    public class GetAllStudentPermissionsListQueryHandler : IRequestHandler<GetAllStudentPermissionsListQuery, List<GetAllStudentPermissionsVm>>
    {
        private readonly IStudentPermissionRepository _studentPermissionRepository;
        private readonly IMapper _mapper;

        public GetAllStudentPermissionsListQueryHandler(IStudentPermissionRepository studentPermissionRepository, IMapper mapper)
        {
            _studentPermissionRepository = studentPermissionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllStudentPermissionsVm>> Handle(GetAllStudentPermissionsListQuery request, CancellationToken cancellationToken)
        {
            var studentPaymentList = await _studentPermissionRepository.GetAllAsync();

            return _mapper.Map<List<GetAllStudentPermissionsVm>>(studentPaymentList);
        }
    }
}
