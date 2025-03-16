using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StonksAPI.DTO.User;
using StonksAPI.Entities;
using StonksAPI.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StonksAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(UserDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings) {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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

        public string GenerateJwt(LoginDto dto)
        {
            // Fetch user from DB using Entity Framework's DbContext
            User user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if(user is null)
            {
                // User is absent in the database
                throw new BadRequestException("Invalid username or password.");
            }
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                // Password hash verification has failed
                throw new BadRequestException("Invalid username or password.");
            }

            // Set up the claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Generate key and signed credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);


            // Create a Jwt Token
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            
            // Return the token
            return tokenHandler.WriteToken(token);

        }
    }
}
