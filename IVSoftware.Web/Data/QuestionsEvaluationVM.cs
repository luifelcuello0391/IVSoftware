using IVSoftware.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Data
{
    public class QuestionsEvaluationVM
    {
        [DisplayName("Id de la Evaluación")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int Id { get; set; }

        [DisplayName("Id de la Evaluación")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public IEnumerable<QuestionEvaluation> QuestionsEvaluation { get; set; }
    }
}