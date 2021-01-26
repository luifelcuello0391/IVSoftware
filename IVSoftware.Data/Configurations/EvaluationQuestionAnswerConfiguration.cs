using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class EvaluationQuestionAnswerConfiguration : BaseEntityMap<EvaluationQuestionAnswer, Guid>
    {
        public EvaluationQuestionAnswerConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<EvaluationQuestionAnswer> builder)
        {
            builder
                .Property(x => x.Answer)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder
                .Property(x => x.IsRight)
                .IsRequired();

            builder
                .Property(x => x.EvaluationQuestionBankId)
                .IsRequired();

            builder
                .HasOne(qa => qa.EvaluationQuestionBank)
                .WithMany(eq => eq.QuestionAnswers)
                .HasForeignKey(qa => qa.EvaluationQuestionBankId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}