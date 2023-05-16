using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VodkaDataAccess;
using VodkaEntities;
namespace VodkaServices.Implementation
{
    public class VodkaUserService : IVodkaUserService
    {
        private UserManager<VodkaUser> _userManager;
        private SignInManager<VodkaUser> _signInManager;
        private IConfiguration _configuration;

        public VodkaUserService(UserManager<VodkaUser> userManager, SignInManager<VodkaUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        public async Task<IdentityResult> RegisterAsync(VodkaUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> SignInAsync(string userName, string password)
        {

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) 
                return String.Empty;
            
            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            if(!result.Succeeded)
            {
                return String.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<VodkaUser> GetUserByJwt(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);

                // Extract claims from the JWT token
                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;
                foreach (Claim claim in claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }
                var userName = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
                if (userName == null)
                    return null;

                var user = await _userManager.FindByNameAsync(userName);
                return user != null ? user : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task InputMoney(string id, decimal money)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && user.isActive == 1)
            {
                user.TotalCash += money;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<IEnumerable<VodkaUser>> GetAll()
        {
            return await _userManager.Users.Where(u => u.isActive == 1)
                .ToListAsync();
        }

        public async Task<VodkaUser?> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && user.isActive == 1)
                return user;
            return null;
        }


        public async Task UpdateById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null && user.isActive == 1)
                await _userManager.UpdateAsync(user);

        }

        public async Task<IdentityResult> DeleteByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.isActive == 0)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Khong tim thay User" });
            }

            user.isActive = 0;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return IdentityResult.Failed(updateResult.Errors.ToArray());
            }
            return IdentityResult.Success;
        }

        public async Task UpdateAsSync(VodkaUser vodkaUser)
        {
            await _userManager.UpdateAsync(vodkaUser);
        }

        public async Task<VodkaUser?> FindByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user !=null  && user.isActive == 1)
                return user;
            return null;
        }
        public async Task<VodkaUser?> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && user.isActive == 1)
                return user;
            return null;
        }
        public async Task<IList<string>> GetRolesAsync(VodkaUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;

        }
        public async Task<IdentityResult> AddToRoleAsync(VodkaUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || user.isActive == 0)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Khong tim thay User" });
            }

            var result = await _signInManager.PasswordSignInAsync(userName, oldPassword, false, false);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Mat khau khong dung" });
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!changePasswordResult.Succeeded)
            {
                return changePasswordResult;
            }

            await _signInManager.SignOutAsync();

            return changePasswordResult;
        }

    }
}
