using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsRight { get; set; }

        [DisplayName("Pregunta")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public Guid EvaluationQuestionBankId { get; set; }

        public virtual EvaluationQuestionBank EvaluationQuestionBank { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}