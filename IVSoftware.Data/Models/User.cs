using Microsoft.AspNetCore.Identity;

namespace IVSoftware.Data.Models
{
    public class User : IdentityUser
    {
        public virtual Person Person { get; set; }
    }
}