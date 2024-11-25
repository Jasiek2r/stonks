using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StonksAPI.DTO;
using StonksAPI.Entities;

namespace StonksAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(UserDbContext userDbContext)
        {
            /*
             * FluentValidation-based field validation which checks
             * whether the data input through the POST request is correct
             */
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Username).NotEmpty();
            RuleFor(e => e.Password).NotEmpty().MinimumLength(6);
            RuleFor(e => e.ConfirmPassword).Equal(e => e.Password);

            // Checks if the email is not taken to prevent two accounts with the same email from registering
            RuleFor(e => e.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = userDbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
