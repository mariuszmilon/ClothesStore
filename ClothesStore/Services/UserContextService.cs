using System.Security.Claims;

namespace ClothesStore.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;

        public int? GetUserId => (int?)int.Parse(User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}

