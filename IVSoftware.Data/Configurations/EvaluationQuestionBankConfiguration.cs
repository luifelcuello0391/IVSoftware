using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class EvaluationQuestionBankConfiguration : BaseEntityMap<EvaluationQuestionBank, Guid>
    {
        public EvaluationQuestionBankConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<EvaluationQuestionBank> builder)
        {
            builder
                .Property(x => x.Question)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(1000);
        }
    }
}