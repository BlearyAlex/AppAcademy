using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.StudentPermissions.Queries.GetStudentPermission
{
    public class GetStudentPermissionQueryHandler : IRequestHandler<GetStudentPermissionQuery, GetStudentPermissionVm>
    {
        private readonly IStudentPermissionRepository _studentPermissionRepository;
        private readonly IMapper _mapper;

        public GetStudentPermissionQueryHandler(IStudentPermissionRepository studentPermissionRepository, IMapper mapper)
        {
            _studentPermissionRepository = studentPermissionRepository;
            _mapper = mapper;
        }

        public async Task<GetStudentPermissionVm> Handle(GetStudentPermissionQuery request, CancellationToken cancellationToken)
        {
            var studentPayment = await _studentPermissionRepository.GetByIdInt(request._StudentPermissionId);

            return _mapper.Map<GetStudentPermissionVm>(studentPayment);
        }
    }
}
