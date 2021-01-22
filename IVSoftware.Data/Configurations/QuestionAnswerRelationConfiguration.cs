using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVSoftware.Data.Configurations
{
    public class QuestionAnswerRelationConfiguration
    {
        public void Map(EntityTypeBuilder<QuestionAnswerRelation> builder)
        {
            builder
                .HasKey(pjr => new { pjr.EvaluationQuestionBankId, pjr.EvaluationQuestionAnswerId });

            builder
                .HasOne(pjr => pjr.EvaluationQuestionBank)
                .WithMany(p => p.QuestionAnswers)
                .HasForeignKey(pjr => pjr.EvaluationQuestionBankId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pjr => pjr.EvaluationQuestionAnswer)
                .WithMany(p => p.QuestionsAnswer)
                .HasForeignKey(pjr => pjr.EvaluationQuestionAnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}