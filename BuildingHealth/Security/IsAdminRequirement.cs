using BuildingHealth.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BuildingHealth.Security
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
        private readonly BuildingHealthDBContext _dbContext;
        private readonly IUserAccessor _httpContextAccessor;

        public IsAdminRequirementHandler(BuildingHealthDBContext dbContext, IUserAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsAdminRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.Email);
            var sd = _httpContextAccessor.GetEmail();
            if (userId == null)
            {
                return Task.CompletedTask;
            }

            var user = _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Email == userId && user.Admin != null).Result;

            if (user == null)
            {
                return Task.CompletedTask;
            }

            if (user.Admin != null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
