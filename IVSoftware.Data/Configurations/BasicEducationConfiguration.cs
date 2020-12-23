using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSoftware.Data.Configurations
{
    public class BasicEducationConfiguration : BaseEntityMap<BasicEducation, Guid>
    {
        public BasicEducationConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<BasicEducation> builder)
        {
            builder
                .Property(x => x.InstitutionName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.LastGradeApproved)
                .IsRequired();

            builder
                .Property(x => x.SpecialtyName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.DegreeDate)
                .IsRequired();

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(g => g.Person)
                .WithMany(p => p.BasicEducations)
                .HasForeignKey(g => g.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
