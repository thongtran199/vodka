using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;
using Vodka.Models.Account;
using VodkaEntities;
using VodkaServices;
using VodkaServices.Implementation;

namespace Vodka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService _accountService;
        public AccountController(IUserService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            var user = new Useraccount {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _accountService.RegisterAsync(user, model.Password);
            if(result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(AccountSignInViewModel model)
        {
            var user = new Useraccount {
                Email = model.Email,
            };
            var result = await _accountService.SignInAsync(user, model.Password);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
    }
    }

}
