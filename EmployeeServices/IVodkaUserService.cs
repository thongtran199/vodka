
using Microsoft.AspNetCore.Identity;
using VodkaEntities;

namespace VodkaServices
{
    public interface IVodkaUserService
    {

        Task<VodkaUser> GetUserByJwt(string jwtString);

        Task<IdentityResult> RegisterAsync(VodkaUser user, string password);
        Task<string> SignInAsync(VodkaUser user, string password);
    }
}
