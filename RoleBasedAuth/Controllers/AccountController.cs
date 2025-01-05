using Application.Core.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using MT.Innovation.WebApiAdmin.Framework;
namespace BlogManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        AccountService _accountService { get; set; }
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin user)
        {
            try
            {
                var token = _accountService.Login(user);
                Response.Cookies.Append("AuthToken", token);
                return Ok(token);
            }
            catch
            {
                return Unauthorized("Incorrect Email/Password");
            }
        }
        [HttpPost("Register")]
        public IActionResult Register(UserRegister user)
        {
            try
            {
                var success = _accountService.Register(user);
                if (success) { 
                return Ok(success);
                }
            }
            catch
            {
                return Unauthorized("Incorrect Email/Password");
            }
            return BadRequest();
            }

    }
}
