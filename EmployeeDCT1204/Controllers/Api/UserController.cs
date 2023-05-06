
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VodkaServices;
using Vodka.Models.TaiKhoan;
using VodkaEntities;

namespace Vodka.Controllers.Api
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _taikhoanService;
        //public UserController(IUserService taiKhoanService)
        //{
        //    _taikhoanService = taiKhoanService;
        //}
        //[HttpPost("DangKy")]
        //public async Task<IActionResult> DangKy(SignUpViewModel model)
        //{
        //    Console.WriteLine("Service Chuan bi xac thuc");
        //    if (ModelState.IsValid)
        //    {
        //        Console.WriteLine("Service Du lieu ok");
        //        var user = new Useraccount()
        //        {
        //            UserName = model.UserName,
        //            Password = model.Password,
        //            AccessLevel = model.AccessLevel,
        //            TotalCash = model.TotalCash,
        //            IsActive = model.IsActive,
        //            Email = model.Email,
        //            Address = model.Address,
        //        };
        //        var result = await _taikhoanService.DangKy(user);
        //        if (result)
        //            return Ok();
        //        return BadRequest("UserName da ton tai");
        //    }
        //    return BadRequest("Xem lai du lieu dau vao !");
        //}

        //[HttpPost("DangNhap")]
        //public async Task<IActionResult> DangNhap(SignInViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new Useraccount
        //        {
        //            UserName = model.UserName,
        //            Password = model.Password
        //        };
        //        var result = await _taikhoanService.DangNhap(user);
        //        if (result != "")
        //        {
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            return Unauthorized("Xac thuc dang nhap khong thanh cong !");
        //            Console.WriteLine("Da kiem tra, khong the dang nhap");
        //        }
        //    }
        //    return Unauthorized("Xem lai du lieu dau vao !");
        //}
    }
}
