using IVSoftware.Data.Models;
using IVSoftware.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IVSoftware.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AdduserAsync(User persona, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}