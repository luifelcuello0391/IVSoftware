using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class SupervisionDetail : BaseModel<Guid>
    {
        [DisplayName("PHVA")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string PHVA { get; set; }

        [DisplayName("Proceso o Subproceso")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Process { get; set; }

        [DisplayName("Actividad supervisada")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string SupervisedActivity { get; set; }

        [DisplayName("Resultado")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Result { get; set; }

        [DisplayName("Observaciones")]
        public string Observations { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid SupervisionId { get; set; }

        public virtual Supervision Supervision { get; set; }
    }
}