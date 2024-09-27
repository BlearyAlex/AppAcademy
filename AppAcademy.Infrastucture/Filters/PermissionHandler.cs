using AppAcademy.Infrastucture.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Filters
{
    public class PermissionHandler : AuthorizationHandler<PermissionAttribute>
    {
        private readonly ILogger<PermissionHandler> _logger;

        public PermissionHandler(ILogger<PermissionHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAttribute requirement)
        {
           if(context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning($"User {context.User.Identity.Name} does not have permission {requirement.Permission}");
            }

           return Task.CompletedTask;
        }
    }
}
