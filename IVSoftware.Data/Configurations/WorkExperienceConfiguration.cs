using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class WorkExperienceConfiguration : BaseEntityMap<WorkExperience, Guid>
    {
        public WorkExperienceConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<WorkExperience> builder)
        {
            builder
                .Property(x => x.CompanyName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.StartDate)
                .IsRequired();

            builder
                .Property(x => x.EndDate)
                .IsRequired(false);

            builder
                .Property(x => x.JobTitle)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.Responsibilities)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(2000);

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(t => t.Person)
                .WithMany(p => p.WorkExperiences)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}