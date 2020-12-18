using IVSoftware.Data.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations.Core
{
    public abstract class BaseEntityMap<TEntityType, TKey> : IEntityTypeMap
        where TEntityType : BaseModel<TKey>
        where TKey : struct
    {
        private readonly string TableName;
        private readonly string IdName;

        public BaseEntityMap(string TableName, string IdName)
        {
            this.TableName = TableName;
            this.IdName = IdName;
        }

        public void Map(ModelBuilder builder)
        {
            #region EntityBase Configuration

            builder.Entity<TEntityType>()
                .ToTable(TableName);

            builder.Entity<TEntityType>()
                .HasKey(x => x.Id);

            builder.Entity<TEntityType>()
                .Property(x => x.Id)
                .HasColumnName(IdName)
                .IsRequired();

            #endregion EntityBase Configuration

            InternalMap(builder.Entity<TEntityType>());
        }

        protected abstract void InternalMap(EntityTypeBuilder<TEntityType> builder);
    }
}