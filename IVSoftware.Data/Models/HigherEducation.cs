using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class HigherEducation : BaseModel<Guid>
    {
        [DisplayName("Nombre de la institución")]
        public string InstitutionName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de grado")]
        public DateTime DegreeDate { get; set; }

        [DisplayName("Nombre de los estudios")]
        public string StudiesName { get; set; }

        [DisplayName("Tarjeta profesional No.")]
        public string ProfessionalCardNumber { get; set; }

        [DisplayName("Modalidad académica")]
        public virtual AcademicLevel AcademicLevel { get; set; }

        [DisplayName("Modalidad académica")]
        public int AcademicLevelId { get; set; }

        public virtual Person Person { get; set; }
        public string PersonId { get; set; }
    }
}