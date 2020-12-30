using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class TrainingConfiguration : BaseEntityMap<Training, Guid>
    {
        public TrainingConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Training> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.Subject)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.CertificationTypeId)
                .IsRequired();

            builder
                .HasOne(t => t.CertificationType)
                .WithMany(ct => ct.Trainings)
                .HasForeignKey(t => t.CertificationTypeId);

            builder
                .Property(x => x.Entity)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.EndDate)
                .IsRequired();

            builder
                .Property(x => x.NumberOfHours)
                .IsRequired();

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(t => t.Person)
                .WithMany(p => p.Trainings)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}