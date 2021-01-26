using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class QuestionEvaluationConfiguration
    {
        public void Map(EntityTypeBuilder<QuestionEvaluation> builder)
        {
            builder
                .HasKey(pjr => new { pjr.QuestionId, pjr.EvaluationId });

            builder
                .HasOne(pjr => pjr.Question)
                .WithMany(p => p.QuestionEvaluations)
                .HasForeignKey(pjr => pjr.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pjr => pjr.Evaluation)
                .WithMany(p => p.QuestionsEvaluation)
                .HasForeignKey(pjr => pjr.EvaluationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}