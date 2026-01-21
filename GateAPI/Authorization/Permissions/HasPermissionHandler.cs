using Microsoft.AspNetCore.Authorization;

namespace GateAPI.Authorization.Permissions
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            if (context.User.Claims.Any(c => c.Type == "permission" && c.Value == requirement.Permissao))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
