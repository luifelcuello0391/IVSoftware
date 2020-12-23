using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class BasicEducation : BaseModel<Guid>
    {
        [DisplayName("Nombre de la institución")]
        public string InstitutionName { get; set; }

        [DisplayName("Último grado aprobado")]
        public int LastGradeApproved { get; set; }

        [DisplayName("Nombre de especialidad")]
        public string SpecialtyName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha de grado")]
        public DateTime DegreeDate { get; set; }

        public virtual Person Person { get; set; }
        public Guid PersonId { get; set; }
    }
}