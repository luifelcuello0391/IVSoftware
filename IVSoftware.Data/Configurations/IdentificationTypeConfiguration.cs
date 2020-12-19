using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class IdentificationTypeConfiguration : BaseEntityMap<IdentificationType, int>
    {
        public IdentificationTypeConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<IdentificationType> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.Code)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(10);
        }
    }
}