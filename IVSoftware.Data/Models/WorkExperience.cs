using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class WorkExperience : BaseModel<Guid>
    {
        [DisplayName("Nombre de la empresa")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string CompanyName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de ingreso")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de retiro")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Cargo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string JobTitle { get; set; }

        [DisplayName("Responsabilidades")]
        [MaxLength(2000, ErrorMessage = "La longitud máxima es de {0} caracteres")]
        public string Responsibilities { get; set; }

        public virtual Person Person { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid PersonId { get; set; }
    }
}