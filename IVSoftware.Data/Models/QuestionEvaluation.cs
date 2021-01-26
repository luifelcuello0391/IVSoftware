using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Data.Models
{
    public class QuestionEvaluation
    {
        public Guid QuestionId { get; set; }

        public virtual EvaluationQuestionBank Question { get; set; }

        public int EvaluationId { get; set; }

        public virtual Evaluation Evaluation { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}