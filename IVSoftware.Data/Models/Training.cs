using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class Training : BaseModel<Guid>
    {
        [DisplayName("Nombre del curso")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Name { get; set; }

        [DisplayName("Tema")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Subject { get; set; }

        [DisplayName("Tipo de certificación")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int CertificationTypeId { get; set; }

        [DisplayName("Tipo de certificación")]
        public virtual CertificationType CertificationType { get; set; }

        [DisplayName("Entidad")]
        public string Entity { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de finalización")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime EndDate { get; set; }

        [DisplayName("Número de horas")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int NumberOfHours { get; set; }

        public virtual Person Person { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid PersonId { get; set; }
    }
}