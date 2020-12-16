using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class EducacionSuperior
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Nombre de la institución")]
        public string NombreInstitucion { get; set; }

        [DisplayName("Semestres aprobados")]
        public int SemestresAprobados { get; set; }

        [DisplayName("Graduado")]
        public bool EsGraduado { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de grado")]
        public DateTime? FechaGrado { get; set; }

        [DisplayName("Nombre de los estudios")]
        public string NombreEstudios { get; set; }

        [DisplayName("Tarjeta profesional No.")]
        public string NumeroTarjetaProfesional { get; set; }

        [DisplayName("Modalidad académica")]
        public virtual ModalidadAcademica ModalidadAcademica { get; set; }

        [DisplayName("Modalidad académica")]
        public int ModalidadAcademicaId { get; set; }

        [DisplayName("Archivo adjunto")]
        public string ArchivoAdjunto { get; set; }

        public virtual Persona Persona { get; set; }
        public string PersonaId { get; set; }
    }
}
