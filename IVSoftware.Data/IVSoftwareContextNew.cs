using IVSoftware.Data.Configurations;
using IVSoftware.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IVSoftware.Data
{
    public class IVSoftwareContextNew : IdentityDbContext<User>
    {
        public IVSoftwareContextNew(DbContextOptions<IVSoftwareContextNew> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ArlConfiguration("Arl", "Id").Map(modelBuilder);
            new BloodTypeConfiguration("BloodType", "Id").Map(modelBuilder);
            new ContractTypeConfiguration("ContractType", "Id").Map(modelBuilder);
            new CountryConfiguration("Country", "Id").Map(modelBuilder);
            new DepartmentConfiguration("Department", "Id").Map(modelBuilder);
            new EpsConfiguration("Eps", "Id").Map(modelBuilder);
            new GenderConfiguration("Gender", "Id").Map(modelBuilder);
            new IdentificationTypeConfiguration("IdentificationType", "Id").Map(modelBuilder);
            new MunicipalityConfiguration("Municipality", "Id").Map(modelBuilder);
            new PersonConfiguration("Person", "Id").Map(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}