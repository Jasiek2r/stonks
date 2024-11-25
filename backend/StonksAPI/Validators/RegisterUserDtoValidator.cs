using FluentValidation;
using StonksAPI.DTO;

namespace StonksAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(e => e.Username).NotEmpty();
            RuleFor(e => e.Password).NotEmpty().MinimumLength(6);
            RuleFor(e => e.ConfirmPassword).Equal(e => e.Password);
        }
    }
}
