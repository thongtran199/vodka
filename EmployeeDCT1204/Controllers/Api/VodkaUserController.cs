
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VodkaServices;
using Vodka.Models.TaiKhoan;
using VodkaEntities;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Text;


namespace Vodka.Controllers.Api
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class VodkaUserController : ControllerBase
    {
        private readonly IVodkaUserService _vodkaUserService;
        public VodkaUserController(IVodkaUserService vodkaUserService)
        {
            _vodkaUserService = vodkaUserService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            var sha256 = new SHA256Managed();
            if (ModelState.IsValid)
            {
                byte[] hashId = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.UserName));
                //var salt = Guid.NewGuid().ToString();
                var user = new VodkaUser()
                {
                    Id = Convert.ToBase64String(hashId),
                    UserName = model.UserName,
                    Email = model.Email,
                    NormalizedUserName = model.UserName.ToUpper(),
                    NormalizedEmail = model.Email?.ToUpper(),
                    AccessLevel = model.AccessLevel,
                    TotalCash = model.TotalCash,
                    Address = model.Address,
                    //Salt = salt,
                };
                var result = await _vodkaUserService.RegisterAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Register Successfull !");
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _vodkaUserService.SignInAsync(model.UserName, model.Password);

                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized();
                }
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
