namespace Users.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class BlockUserRequirement : IAuthorizationRequirement
    {
        public BlockUserRequirement(params string[] users) {
            BlockedUsers = users;
        }
        
        public string[] BlockedUsers { get; set; }
    }
    
    public class BlockUserHandler : AuthorizationHandler<BlockUserRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BlockUserRequirement requirement)
        {
            if (context.User.Identity?.Name != null)
            {
                if (requirement.BlockedUsers.Any(x => x.Equals(context.User.Identity.Name)))
                {
                    context.Fail();
                }
                else
                {
                    context.Succeed(requirement);
                }
            }

            await Task.CompletedTask;
        }
    }
}