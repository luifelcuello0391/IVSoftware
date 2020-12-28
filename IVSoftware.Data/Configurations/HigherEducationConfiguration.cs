using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class HigherEducationConfiguration : BaseEntityMap<HigherEducation, Guid>
    {
        public HigherEducationConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<HigherEducation> builder)
        {
            builder
                 .Property(x => x.InstitutionName)
                 .IsRequired()
                 .IsUnicode()
                 .HasMaxLength(255);

            builder
                .Property(x => x.StudiesName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.DegreeDate)
                .IsRequired();

            builder
                .Property(x => x.ProfessionalCardNumber)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(g => g.Person)
                .WithMany(p => p.HigherEducations)
                .HasForeignKey(g => g.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(g => g.AcademicLevel)
                .WithMany(p => p.HigherEducations)
                .HasForeignKey(g => g.AcademicLevelId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}