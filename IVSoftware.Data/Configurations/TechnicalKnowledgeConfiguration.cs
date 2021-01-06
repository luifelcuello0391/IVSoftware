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
                .Property(x => x.ServiceId)
                .IsRequired();

            builder
                .HasOne(t => t.Service)
                .WithMany(s => s.TechnicalKnowledges)
                .HasForeignKey(t => t.ServiceId);

            builder
                .Property(x => x.AnalyticTechnique)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.MatrixId)
                .IsRequired();

            builder
                .HasOne(t => t.Matrix)
                .WithMany(m => m.TechnicalKnowledges)
                .HasForeignKey(t => t.MatrixId);

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