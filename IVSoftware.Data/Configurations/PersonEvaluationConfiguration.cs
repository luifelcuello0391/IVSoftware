using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class PersonEvaluationConfiguration : BaseEntityMap<PersonEvaluation, Guid>
    {
        public PersonEvaluationConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<PersonEvaluation> builder)
        {
            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .Property(x => x.EvaluationId)
                .IsRequired();

            builder
                .Property(x => x.ResultJson)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(int.MaxValue);

            builder
                .Property(x => x.AssignedDate)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .IsRequired(false);

            builder
                .Property(x => x.Score)
                .IsRequired(false);

            builder
                .Property(x => x.IsApproved)
                .IsRequired(false);

            builder
                .HasOne(pjr => pjr.Person)
                .WithMany(p => p.PersonEvaluations)
                .HasForeignKey(pjr => pjr.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pjr => pjr.Evaluation)
                .WithMany(p => p.PersonEvaluations)
                .HasForeignKey(pjr => pjr.EvaluationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}