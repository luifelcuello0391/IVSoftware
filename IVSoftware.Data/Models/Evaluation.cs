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

        public virtual ICollection<PersonEvaluation> PersonEvaluations { get; set; }
    }
}