using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class NotificationSettingConfiguration : BaseEntityMap<NotificationSetting, int>
    {
        public NotificationSettingConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<NotificationSetting> builder)
        {
            builder
                .Property(x => x.Email)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);
        }
    }
}