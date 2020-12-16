using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class Formacion
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Nombre del curso")]
        public string NombreCurso { get; set; }

        [DisplayName("Tema")]
        public string Tema { get; set; }

        [DisplayName("Tipo de certificación")]
        public virtual TipoCertificacion TipoCertificacion { get; set; }

        [DisplayName("Tipo de certificación")]
        public int TipoCertificacionId { get; set; }

        [DisplayName("Entidad")]
        public string Entidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de finalización")]
        public DateTime FechaFinalizacion { get; set; }

        [DisplayName("Número de horas")]
        public int NumeroHoras { get; set; }

        [DisplayName("Archivo adjunto")]
        public string ArchivoAdjunto { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }
    }
}
