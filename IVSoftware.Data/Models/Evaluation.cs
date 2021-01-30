using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class Evaluation : BaseModel<int>
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Name { get; set; }

        [DisplayName("Fecha de Evaluación")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime Date { get; set; }

        [DisplayName("Periodicidad")]
        public int? PeriodicityId { get; set; }

        [DisplayName("Periodicidad")]
        public virtual Periodicity Periodicity{ get; set; }

        [DisplayName("Cantidad")]
        public int? PeriodicityAmount { get; set; }

        [DisplayName("Porcentaje para aprobar")]
        [Range(50, 100, ErrorMessage = "El Porcentaje para aprobar debe estar entre 50 y 100 porciento")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int PercentageToPass { get; set; } = 50;

        public virtual ICollection<PersonEvaluation> PersonEvaluations { get; set; }

        public virtual ICollection<QuestionEvaluation> QuestionsEvaluation { get; set; }
    }
}