using Microsoft.AspNetCore.Identity;
using StonksAPI.DTO;
using StonksAPI.Entities;

namespace StonksAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(UserDbContext context, IPasswordHasher<User> passwordHasher) {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Login()
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //create new user
            User newUser = new User
            {
                Email = registerUserDto.Email,
                Username = registerUserDto.Username
            };

            // hash and set user password
            string hashedPassword = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = hashedPassword;

            //save user to database
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
