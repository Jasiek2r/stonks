using Microsoft.AspNetCore.Mvc;
using StonksAPI.DTO.User;
using StonksAPI.Services;

namespace StonksAPI.Controllers
{
    [ApiController]

    [Route("auth")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
