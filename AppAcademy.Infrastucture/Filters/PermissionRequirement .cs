using Microsoft.AspNetCore.Authorization;

namespace AppAcademy.Application.Filters
{
    public class PermissionAttribute : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
