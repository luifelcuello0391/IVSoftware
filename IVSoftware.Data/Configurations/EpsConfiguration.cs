using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class EpsConfiguration : BaseEntityMap<Eps, int>
    {
        public EpsConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Eps> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);
        }
    }
}