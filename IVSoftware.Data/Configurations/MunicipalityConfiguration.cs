using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class MunicipalityConfiguration : BaseEntityMap<Municipality, int>
    {
        public MunicipalityConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Municipality> builder)
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

            builder
                .Property(x => x.DepartmentId)
                .IsRequired(false);

            builder
                .HasOne(d => d.Department)
                .WithMany(m => m.Municipalities)
                .HasForeignKey(d => d.DepartmentId);
        }
    }
}