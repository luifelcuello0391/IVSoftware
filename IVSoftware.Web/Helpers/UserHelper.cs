using IVSoftware.Data.Models;
using IVSoftware.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IVSoftware.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AdduserAsync(User persona, string password)
        {
            return await this.userManager.CreateAsync(persona, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByNameAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.UserName,
                ClsCipher.Encrypt(model.Password, ClsCipher.PasswordKey),
                false,
                false);
        }

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}