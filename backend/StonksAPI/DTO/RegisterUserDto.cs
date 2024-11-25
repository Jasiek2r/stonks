using System.ComponentModel.DataAnnotations;

namespace StonksAPI.DTO
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
