using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class PersonEvaluationConfiguration
    {
        public void Map(EntityTypeBuilder<PersonEvaluation> builder)
        {
            builder
                .HasKey(pjr => new { pjr.PersonId, pjr.EvaluationId });

            builder
                .Property(x => x.ResultJson)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(int.MaxValue);

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