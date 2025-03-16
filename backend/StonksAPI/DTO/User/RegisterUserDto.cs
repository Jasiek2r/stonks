using System.ComponentModel.DataAnnotations;

namespace StonksAPI.DTO.User
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
