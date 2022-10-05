using ClothesStore.Models;

namespace ClothesStore.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginUserDto dto);
        void RegisterUser(RegisterUserDto dto);
    }
}
