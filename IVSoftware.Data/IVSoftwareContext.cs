using IVSoftware.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IVSoftware.Data
{
    public class IVSoftwareContext : IdentityDbContext<User>
    {
        public IVSoftwareContext(DbContextOptions<IVSoftwareContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}