using AppAcademy.Infrastucture.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AppAcademy.Application.Filters
{
    public class PermissionHandler : AuthorizationHandler<PermissionAttribute>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAttribute requirement)
        {
           if(context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }

           return Task.CompletedTask;
        }
    }
}
