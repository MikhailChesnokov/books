namespace Users.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Models;

    public class DocumentAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool AllowAuthors { get; set; }

        public bool AllowEditors { get; set; }
    }
    
    public class DocumentAuthorizationHandler : AuthorizationHandler<DocumentAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DocumentAuthorizationRequirement requirement)
        {
            ProtectedDocument doc = context.Resource as ProtectedDocument;
            
            string user = context.User.Identity.Name;

            
            
            if (doc != null && user != null &&
                (
                    requirement.AllowAuthors && doc.Author.Equals(user) ||
                    requirement.AllowEditors && doc.Editor.Equals(user)
                ))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            await Task.CompletedTask;
        }
    }
}