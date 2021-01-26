using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class EvaluationQuestionBank : BaseModel<Guid>
    {
        [DisplayName("Pregunta")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [MaxLength(1000, ErrorMessage = "La {0} debe tener máximo {1} caracteres")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres")]
        public string Question { get; set; }

        public virtual ICollection<EvaluationQuestionAnswer> QuestionAnswers { get; set; }

        public virtual ICollection<QuestionEvaluation> QuestionEvaluations { get; set; }
    }
}