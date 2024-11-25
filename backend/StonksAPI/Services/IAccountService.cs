using StonksAPI.DTO;

namespace StonksAPI.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto registerUserDto);
        public void Login();
        string GenerateJwt(LoginDto dto);
    }
}
