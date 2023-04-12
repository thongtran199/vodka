using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;

namespace VodkaServices
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(Useraccount user, string password);
        Task<string> SignInAsync(Useraccount user, string password);
    }
}
