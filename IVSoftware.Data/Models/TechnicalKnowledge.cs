using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class TechnicalKnowledge : BaseModel<Guid>
    {
        [DisplayName("Análisis")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Service { get; set; }

        [DisplayName("Técnica analítica")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string AnalyticTechnique { get; set; }

        [Required(ErrorMessage = "La {0} es requerida.")]
        [DisplayName("Matriz")]
        public string Matrix { get; set; }

        [DisplayName("Time")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Time { get; set; }

        public virtual Person Person { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid PersonId { get; set; }
    }
}