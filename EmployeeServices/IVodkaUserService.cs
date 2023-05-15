
using Microsoft.AspNetCore.Identity;
using VodkaEntities;

namespace VodkaServices
{
    public interface IVodkaUserService
    {

        Task<string> SignInAsync(string userName, string password);
        Task<VodkaUser> GetUserByJwt(string jwtString);

        Task<IdentityResult> RegisterAsync(VodkaUser user, string password);
        

        Task InputMoney(string id, decimal money);


        Task<IEnumerable<VodkaUser>> GetAll();
        Task<VodkaUser> GetById(string id);
        Task UpdateById(string id);
        Task DeleteById(string id);
        Task UpdateAsSync(VodkaUser vodkauser);

        Task<VodkaUser> FindByNameAsync(string userName);

        Task<VodkaUser> FindByIdAsync(string userId);

        Task<IList<string>> GetRolesAsync(VodkaUser user);

        Task<IdentityResult> AddToRoleAsync(VodkaUser user, string roleName);


        Task<IdentityResult> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
    }
}
