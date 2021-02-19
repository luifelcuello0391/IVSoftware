using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class Supervision : BaseModel<Guid>
    {
        [DisplayName("Persona supervisada")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid SupervisedPersonId { get; set; }

        [DisplayName("Persona supervisada")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public virtual Person SupervisedPerson { get; set; }

        [DisplayName("Supervisado por")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid SupervisedById { get; set; }

        [DisplayName("Supervisado por")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public virtual Person SupervisedBy { get; set; }

        [DisplayName("Fecha de supervisión")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public DateTime Date { get; set; }

        [DisplayName("Analito o mensurando involucrado")]
        public string Analyte { get; set; }

        public virtual ICollection<SupervisionDetail> Details { get; set; }
    }
}