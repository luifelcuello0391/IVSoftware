using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class EducacionBasica
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Nombre de la institución")]
        public string NombreInstitucion { get; set; }

        [DisplayName("Último grado aprobado")]
        public int UltimoGradoAprobado { get; set; }

        [DisplayName("Nombre de especialidad")]
        public string TituloObtenido { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha de grado")]
        public DateTime FechaGrado { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }
    }
}
