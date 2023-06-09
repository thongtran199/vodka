﻿
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
using MySqlX.XDevAPI.Common;
using Microsoft.AspNetCore.Authorization;

namespace Vodka.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VodkaUserController : ControllerBase
    {
        private readonly IVodkaUserService _vodkaUserService;
        private readonly ITransactheaderService _transactheaderService;
        public VodkaUserController(IVodkaUserService vodkaUserService, ITransactheaderService transactheaderService)
        {
            _vodkaUserService = vodkaUserService;
            _transactheaderService = transactheaderService;
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
                    isActive = 1
                };
                var result = await _vodkaUserService.RegisterAsync(user, model.Password);
                if (result.Succeeded)
                {
                    foreach (var role in model.Roles)
                    {
                        string roleName = "";
                        if (role == 50)
                        {
                            roleName = "Admin";
                        }
                        else if (role == 100)
                        {
                            roleName = "Client";
                        }
                        if (!String.IsNullOrEmpty(roleName))
                            await _vodkaUserService.AddToRoleAsync(user, roleName);
                    }
                    await _transactheaderService.CreateNewEmptyTransactheader(user.Id);
                    return Ok("Register Successfull !");
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (String.IsNullOrEmpty(model.UserName) || String.IsNullOrEmpty(model.Password))
            {
                return Unauthorized("Input bi sai !");
            }
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
                    user = modelUser,
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
        public async Task<IActionResult> GetAll()
        {
            var listUser = new List<VodkaUserIndexViewModel>();
            var list = await _vodkaUserService.GetAll(); //await the GetAll() method
            foreach (var user in list)
            {
                var roles = await _vodkaUserService.GetRolesAsync(user);
                var indexUser = new VodkaUserIndexViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    NormalizedEmail = user.NormalizedEmail,
                    NormalizedUserName = user.NormalizedUserName,
                    Address = user.Address,
                    AccessLevel = user.AccessLevel,
                    TotalCash = user.TotalCash,
                    LockoutEnabled = user.LockoutEnabled,
                    Roles = roles,
                    isActive = user.isActive
                    
                };
                listUser.Add(indexUser);
            }
            return Ok(listUser);
        }

        [HttpGet("GetVodkaUserById/{id}")]
        public async Task<IActionResult> GetVodkaUserById(string id)
        {
            var user = await _vodkaUserService.GetById(id);
            if (user == null)
                return NotFound();
            var roles = await _vodkaUserService.GetRolesAsync(user);
            var detailUser = new VodkaUserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                Address = user.Address,
                ConcurrencyStamp = user.ConcurrencyStamp,
                SecurityStamp = user.SecurityStamp,
                AccessLevel = user.AccessLevel,
                TotalCash = user.TotalCash,
                LockoutEnabled = user.LockoutEnabled,
                Roles = roles,
                isActive = user.isActive
            };
            return Ok(detailUser);
        }

        [HttpGet("GetVodkaUserByUserName/{username}")]
        public async Task<IActionResult> GetVodkaUserByUserName(string username)
        {
            var user = await _vodkaUserService.FindByNameAsync(username);
            if (user == null)
                return NotFound();
            var roles = await _vodkaUserService.GetRolesAsync(user);
            var detailUser = new VodkaUserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                ConcurrencyStamp = user.ConcurrencyStamp,
                SecurityStamp = user.SecurityStamp,
                Address = user.Address,
                AccessLevel = user.AccessLevel,
                TotalCash = user.TotalCash,
                LockoutEnabled = user.LockoutEnabled,
                Roles = roles,
                isActive = user.isActive
            };
            return Ok(detailUser);
        }

        [HttpPut("UpdateAsSync")]
        public async Task<IActionResult> UpdateAsSync([FromBody] VodkaUserEditViewModel model)
        {
            if (model == null || model.Id == null)
                return BadRequest();
            
            var user = await _vodkaUserService.GetById(model.Id);
            if (user == null)
                return NotFound();

            user.Address = model.Address;
            user.TotalCash = model.TotalCash;
            user.Email = model.Email;
            user.NormalizedEmail = model.Email?.ToUpper();

            await _vodkaUserService.UpdateAsSync(user);
            return Ok();
        }

        [HttpPut("ChangePasswordASync")]
        public async Task<IActionResult> ChangePasswordASync([FromBody] ChangePasswordViewModel model)
        {
            if (String.IsNullOrEmpty(model.userName) || String.IsNullOrEmpty(model.oldPassword) || String.IsNullOrEmpty(model.newPassword))
            {
                return BadRequest();
            }
            var result = await _vodkaUserService.ChangePasswordAsync(model.userName, model.oldPassword, model.newPassword);
            if (result.Succeeded)
                return Ok();
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [HttpPut("DeleteVodkaUserASync")]
        public async Task<IActionResult> DeleteVodkaUserASync(string userId)
        {
            if (String.IsNullOrEmpty(userId.Trim()))
            {
                return BadRequest("Chua dien thong tin !");
            }
            var result = await _vodkaUserService.DeleteByIdAsync(userId);
            if (result.Succeeded)
                return Ok();
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [HttpGet("GetTotalAdminActive")]
        public async Task<IActionResult> GetTotalAdminActive()
        {
            var result = await _vodkaUserService.GetTotalAdminActive();
            return Ok(result);
        }
        [HttpGet("GetTotalAdminInActive")]
        public async Task<IActionResult> GetTotalAdminInActive()
        {
            var result = await _vodkaUserService.GetTotalAdminInActive();
            return Ok(result);
        }
        [HttpGet("GetTotalClientActive")]
        public async Task<IActionResult> GetTotalClientActive()
        {
            var result = await _vodkaUserService.GetTotalClientActive();
            return Ok(result);
        }
        [HttpGet("GetTotalClientInActive")]
        public async Task<IActionResult> GetTotalClientInActive()
        {
            var result = await _vodkaUserService.GetTotalClientInActive();
            return Ok(result);
        }
        [HttpGet("OnlyAdminCanAccessTo")]
        [Authorize]
        public async Task<IActionResult> OnlyAdminCanAccessTo()
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _vodkaUserService.TestXacThucVoiJwt(jwtToken);
            if (result.Succeeded)
            {
                return Ok("Test thanh cong \n Ban la admin");
            }
            else
                return Unauthorized(result.Errors);
        }
    }


}
