
using VodkaEntities;

namespace VodkaServices
{
    public interface ITaiKhoanService
    {
        Task<Boolean> DangKy(Useraccount user);
        Task<string> DangNhap(Useraccount user);
        Task<Useraccount> GetUserByJwt(string jwtString);
    }
}
