using Microsoft.AspNetCore.Mvc;
using StonksAPI.DTO;
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
    }
}
