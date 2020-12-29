using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class HigherEducation : BaseModel<Guid>
    {
        [DisplayName("Nombre de la institución")]

        [Required(ErrorMessage = "El {0} es requerido.")]
        public string InstitutionName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de grado")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime DegreeDate { get; set; }

        [DisplayName("Nombre de los estudios")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string StudiesName { get; set; }

        [DisplayName("Tarjeta profesional No.")]
        public string ProfessionalCardNumber { get; set; }

        [DisplayName("Modalidad académica")]
        public virtual AcademicLevel AcademicLevel { get; set; }

        [DisplayName("Modalidad académica")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int AcademicLevelId { get; set; }

        public virtual Person Person { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid PersonId { get; set; }
    }
}