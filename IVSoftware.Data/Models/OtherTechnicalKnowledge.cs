using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class OtherTechnicalKnowledge : BaseModel<Guid>
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Name { get; set; }

        [DisplayName("Tiempo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Time { get; set; }

        public virtual Person Person { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid PersonId { get; set; }
    }
}