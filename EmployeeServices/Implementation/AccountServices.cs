using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace VodkaServices.Implementation
{
    public class AccountServices : IUserService
    {
        private UserManager<Useraccount> _userManager;
        private SignInManager<Useraccount> _signInManager;
        private IConfiguration _configuration;

        public AccountServices(UserManager<Useraccount> userManager, SignInManager<Useraccount> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        public async Task<IdentityResult> RegisterAsync(Useraccount user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> SignInAsync(Useraccount user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, password, false, false);
            if (!result.Succeeded)
            {
                Console.WriteLine("Dang nhap khong thanh cong !");
                return String.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var secretkey = _configuration["JWT:Secret"];
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
