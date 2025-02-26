using MediatR;

namespace AppAcademy.Application.Features.Permissions.Queries.GetPermission
{
    public class GetPermissionQuery : IRequest<GetPermissionVm>
    {
        public int _PermissionId { get; set; }

        public GetPermissionQuery(int permissionId)
        {
            _PermissionId = permissionId;
        }
    }
}
