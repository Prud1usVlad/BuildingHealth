using Microsoft.AspNetCore.Authorization;

namespace BuildingHealth.Security
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
        protected internal bool IsAdmin { get; set; }

        public IsAdminRequirement(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsAdminRequirement requirement)
        {
            var isAdmin = context.User.HasClaim(c => c.Type == "Admin");
            if (isAdmin != requirement.IsAdmin)
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}
