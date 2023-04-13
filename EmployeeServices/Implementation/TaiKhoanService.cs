using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VodkaDataAccess;
using VodkaEntities;
namespace VodkaServices.Implementation
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private ApplicationDbContext _context;
        public TaiKhoanService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Boolean> DangKy(Useraccount user)
        {
            var result = _context.Useraccounts.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (result == null)
            {
                user.UserId = user.UserName + user.UserPassword;
                _context.Useraccounts.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<string> DangNhap(Useraccount user)
        {
            var result = _context.Useraccounts.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (result != null)
            {
                var claims = new[]{
                        new Claim(ClaimTypes.Name, user.UserName)
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@VanthongSGU19092002"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(

                    issuer: "MyIssuer",
                    audience: "MyAudience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return "";
        }

        public async Task<Useraccount> GetUserByJwt(string jwtString)
        {
            string token = jwtString;

            // The secret key used for encoding the JWT token
            string secretKey = "@VanthongSGU19092002";

            // Decode the JWT token and validate its signature
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
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

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
                var user = _context.Useraccounts.Where(x => x.UserName == userName).FirstOrDefault();
                return user != null ? user : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
