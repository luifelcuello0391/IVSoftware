using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class PersonJobRoleConfiguration
    {
        public void Map(EntityTypeBuilder<PersonJobRole> builder)
        {
            builder
                .HasKey(pjr => new { pjr.PersonId, pjr.JobRoleId });

            builder
                .HasOne(pjr => pjr.Person)
                .WithMany(p => p.PeopleJobRole)
                .HasForeignKey(pjr => pjr.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pjr => pjr.JobRole)
                .WithMany(p => p.PeopleJobRole)
                .HasForeignKey(pjr => pjr.JobRoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}