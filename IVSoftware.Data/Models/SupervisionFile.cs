using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class SupervisionFile : BaseModel<Guid>
    {
        [DisplayName("Ruta")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Path { get; set; }

        [DisplayName("Fecha")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime Date { get; set; }

        [DisplayName("Cargado por")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string UploadedBy { get; set; }

        [DisplayName("Supervisión")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public Guid SupervisionId { get; set; }

        [DisplayName("Supervisión")]
        public virtual Supervision Supervision { get; set; }
    }
}