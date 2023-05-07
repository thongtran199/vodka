
using Microsoft.AspNetCore.Identity;
using VodkaEntities;

namespace VodkaServices
{
    public interface IVodkaUserService
    {

        Task<VodkaUser> GetUserByJwt(string jwtString);

        Task<IdentityResult> RegisterAsync(VodkaUser user, string password);
        Task<string> SignInAsync(string userName, string password);

        Task InputMoney(string id, decimal money);


        IEnumerable<VodkaUser> GetAll();
        Task<VodkaUser> GetById(string id);
        Task UpdateById(string id);
        Task DeleteById(string id);
        Task UpdateAsSync(VodkaUser vodkauser);

        Task<VodkaUser> GetVodkaUserByUserName(string username);

    }
}
