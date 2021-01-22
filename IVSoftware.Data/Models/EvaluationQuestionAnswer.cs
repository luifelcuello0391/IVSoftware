using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class EvaluationQuestionAnswer : BaseModel<Guid>
    {
        [DisplayName("Respuesta")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [MaxLength(500, ErrorMessage = "La {0} debe tener máximo {1} caracteres")]
        public string Answer { get; set; }

        [DisplayName("¿Es Correcta?")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string IsRight { get; set; }

        public virtual ICollection<QuestionAnswerRelation> QuestionsAnswer { get; set; }
    }
}