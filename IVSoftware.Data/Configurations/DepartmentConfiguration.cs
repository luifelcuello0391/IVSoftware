using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class DepartmentConfiguration : BaseEntityMap<Department, int>
    {
        public DepartmentConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Department> builder)
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