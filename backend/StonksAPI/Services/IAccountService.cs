using StonksAPI.DTO.User;

namespace StonksAPI.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto registerUserDto);
        string GenerateJwt(LoginDto dto);
    }
}
