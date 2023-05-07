
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VodkaServices;
using VodkaEntities;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Vodka.Models.VodkaUser;
using Vodka.Models.Taxinfo;
using VodkaServices.Implementation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VodkaDataAccess;
using System.Data;

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
                };

                var result = await _vodkaUserService.RegisterAsync(user, model.Password);
                if (result.Succeeded)
                {
                    foreach (var role in model.Roles)
                    {
                        string roleName = "";
                        if (role == 0)
                        {
                            roleName = "Admin";
                        }
                        else if (role == 1)
                        {
                            roleName = "Client";
                        }
                        if (!String.IsNullOrEmpty(roleName))
                            await _vodkaUserService.AddToRoleAsync(user, roleName);
                    }

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

                var user = await _vodkaUserService.FindByNameAsync(model.UserName);
                var modelUser = new VodkaUserDetailViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    AccessLevel = user.AccessLevel,
                    Address = user.Address,
                    TotalCash = user.TotalCash
                };
                var roles = await _vodkaUserService.GetRolesAsync(user);

                var response = new
                {
                    result = result,
                    user = user,
                    roles = roles,
                };

                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPut("InputMoney")]
        public async Task<IActionResult> InputMoney(string userId, decimal money)
        {
            if (ModelState.IsValid)
            {
                await _vodkaUserService.InputMoney(userId, money);
                return Ok();
            }
            return BadRequest();
        }



        [HttpGet("GetAllVodkaUsers")]
        public IActionResult GetAll()
        {
            var list = _vodkaUserService.GetAll();
            return Ok(list);
        }

        [HttpGet("GetVodkaUserById/{id}")]
        public IActionResult GetVodkaUserById(string id)
        {
            var user = _vodkaUserService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("GetVodkaUserByUserName/{username}")]
        public IActionResult GetVodkaUserByUserName(string username)
        {
            var user = _vodkaUserService.FindByNameAsync(username);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] VodkaUserEditViewModel model)
        {
            if (model == null)
                return BadRequest();

            var user = await _vodkaUserService.GetById(model.Id);
            if (user == null)
                return NotFound();

            user.Address = model.Address;
            user.TotalCash = model.TotalCash;

            await _vodkaUserService.UpdateAsSync(user);
            return Ok();
        }
    }
}
