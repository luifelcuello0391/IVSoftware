using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class TechnicalKnowledgeConfiguration : BaseEntityMap<TechnicalKnowledge, Guid>
    {
        public TechnicalKnowledgeConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<TechnicalKnowledge> builder)
        {
            builder
                .Property(x => x.Service)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.AnalyticTechnique)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.Matrix)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.Time)
                .IsRequired(true)
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(t => t.Person)
                .WithMany(p => p.TechnicalKnowledges)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}