using StonksAPI.DTO;

namespace StonksAPI.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto registerUserDto);
        string GenerateJwt(LoginDto dto);
    }
}
