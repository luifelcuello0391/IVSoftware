using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class SupervisionFileConfiguration : BaseEntityMap<SupervisionFile, Guid>
    {
        public SupervisionFileConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<SupervisionFile> builder)
        {
            builder
                .Property(x => x.Path)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(1000);

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.UploadedBy)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.SupervisionId)
                .IsRequired();

            builder
                .HasOne(x => x.Supervision)
                .WithMany(p => p.Files)
                .HasForeignKey(x => x.SupervisionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}