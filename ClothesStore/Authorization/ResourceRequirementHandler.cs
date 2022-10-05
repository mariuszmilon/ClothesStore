using Microsoft.AspNetCore.Authorization;
using ClothesStore.Entities;
using System.Security.Claims;

namespace ClothesStore.Authorization
{
    public class ResourceRequirementHandler : AuthorizationHandler<ResourceRequirement, Item>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceRequirement requirement, Item resource)
        {
            if (requirement.Operation == ResourceOperation.Get || requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (resource.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
