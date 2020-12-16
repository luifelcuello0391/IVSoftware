using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class AplicacionEvaluacion
    {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de aplicación")]
        public DateTime FechaAplicacion { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }

        public virtual Evaluacion Evaluacion { get; set; }
        public int EvaluacionId { get; set; }
    }
}
