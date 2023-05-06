using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VodkaDataAccess;
using VodkaEntities;
namespace VodkaServices.Implementation
{
    public class VodkaUserService : IVodkaUserService
    {
        private ApplicationDbContext _context;

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


        //public async Task<Boolean> DangKy(Useraccount user)
        //{
        //    var result = _context.Useraccounts.Where(x => x.UserName == user.UserName).FirstOrDefault();
        //    if (result == null)
        //    {
        //        user.UserId = user.UserName + user.Password;
        //        _context.Useraccounts.Add(user);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<string> DangNhap(Useraccount user)
        //{
        //    var result = _context.Useraccounts.Where(x => x.UserName == user.UserName).FirstOrDefault();
        //    if (result != null)
        //    {
        //        var claims = new[]{
        //                new Claim(ClaimTypes.Name, user.UserName)
        //            };
        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@VanthongSGU19092002"));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        var token = new JwtSecurityToken(

        //            issuer: "MyIssuer",
        //            audience: "MyAudience",
        //            claims: claims,
        //            expires: DateTime.Now.AddMinutes(30),
        //            signingCredentials: creds
        //        );
        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }
        //    return "";
        //}

        public async Task<string> SignInAsync(VodkaUser user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, password, false, false);
            if (!result.Succeeded)
            {
                return String.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

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

            string secretKey = "@VanthongSGU19092002";

            var tokenHandler = new JwtSecurityTokenHandler();
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
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
                var user = _userManager.GetU
                var user = _context.VodkaUser.Where(x => x.UserName == userName).FirstOrDefault();
                return user != null ? user : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
