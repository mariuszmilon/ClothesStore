using System.Security.Claims;
using System.Security.Principal;

namespace ClothesStore.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}
