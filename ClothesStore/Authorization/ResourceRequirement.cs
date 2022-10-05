using Microsoft.AspNetCore.Authorization;

namespace ClothesStore.Authorization
{
    public class ResourceRequirement : IAuthorizationRequirement
    {
        public ResourceOperation Operation { get; }

        public ResourceRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }
    }
}
