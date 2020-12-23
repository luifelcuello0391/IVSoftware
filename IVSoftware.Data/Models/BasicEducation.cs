using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class BasicEducation : BaseModel<Guid>
    {
        [DisplayName("Nombre de la institución")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string InstitutionName { get; set; }

        [DisplayName("Último grado aprobado")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int LastGradeApproved { get; set; }

        [DisplayName("Nombre de especialidad")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string SpecialtyName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha de grado")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime DegreeDate { get; set; }

        public virtual Person Person { get; set; }
        public Guid PersonId { get; set; }
    }
}