using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class PersonaInduccion
    {
        [Key]
        public long Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha")]
        public DateTime FechaInduccion { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }

        public virtual TipoInduccion TipoInduccion { get; set; }

        [DisplayName("Tipo de inducción")]
        public int TipoInduccionId { get; set; }
    }
}