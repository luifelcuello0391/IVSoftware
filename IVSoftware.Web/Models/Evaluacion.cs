using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class Evaluacion
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual List<Pregunta> ListPregunta { get; set; }
    }
}
