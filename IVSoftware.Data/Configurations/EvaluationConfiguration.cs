using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class EvaluationConfiguration : BaseEntityMap<Evaluation, int>
    {
        public EvaluationConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Evaluation> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.PeriodicityId)
                .IsRequired(false);

            builder
                .HasOne(e => e.Periodicity)
                .WithMany(p => p.Evaluations)
                .HasForeignKey(e => e.PeriodicityId);

            builder
                .Property(x => x.PeriodicityAmount)
                .IsRequired(false);
        }
    }
}