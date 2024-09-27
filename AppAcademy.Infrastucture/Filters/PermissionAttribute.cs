using Microsoft.AspNetCore.Authorization;

namespace AppAcademy.Infrastucture.Filters
{
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
