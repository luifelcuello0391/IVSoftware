using System;

namespace IVSoftware.Data.Models
{
    public class QuestionAnswerRelation
    {
        public Guid EvaluationQuestionBankId { get; set; }

        public virtual EvaluationQuestionBank EvaluationQuestionBank { get; set; }

        public Guid EvaluationQuestionAnswerId { get; set; }

        public virtual EvaluationQuestionAnswer EvaluationQuestionAnswer { get; set; }
    }
}