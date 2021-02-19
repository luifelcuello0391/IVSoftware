using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class SupervisionConfiguration : BaseEntityMap<Supervision, Guid>
    {
        public SupervisionConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Supervision> builder)
        {
            builder
                .Property(x => x.SupervisedPersonId)
                .IsRequired();

            builder
                .HasOne(x => x.SupervisedPerson)
                .WithMany(p => p.Supervisions)
                .HasForeignKey(x => x.SupervisedPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(x => x.SupervisedById)
                .IsRequired();

            builder
                .HasOne(x => x.SupervisedBy)
                .WithMany(p => p.SupervisionsByMe)
                .HasForeignKey(x => x.SupervisedById)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.Analyte)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(500);
        }
    }
}