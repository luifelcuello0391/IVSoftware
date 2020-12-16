using IVSoftware.Web.Data;
using IVSoftware.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IVSoftware.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<Persona> userManager;
        private readonly SignInManager<Persona> signInManager;

        public UserHelper(UserManager<Persona> userManager, SignInManager<Persona> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AdduserAsync(Persona persona, string password)
        {
            return await this.userManager.CreateAsync(persona, password);
        }

        public async Task<Persona> GetUserByEmailAsync(string email)
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
