using IVSoftware.Web.Data;
using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IVSoftware.Web.Helpers
{
    public interface IUserHelper
    {
        Task<Persona> GetUserByEmailAsync(string email);
        Task<IdentityResult> AdduserAsync(Persona persona, string password);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
