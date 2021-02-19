using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class SupervisionDetailConfiguration : BaseEntityMap<SupervisionDetail, Guid>
    {
        public SupervisionDetailConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<SupervisionDetail> builder)
        {
            builder
                .Property(x => x.PHVA)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.Process)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.SupervisedActivity)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(3000);

            builder
                .Property(x => x.Result)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.Observations)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(5000);

            builder
                .Property(x => x.SupervisionId)
                .IsRequired();

            builder
                .HasOne(x => x.Supervision)
                .WithMany(p => p.Details)
                .HasForeignKey(x => x.SupervisionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}