using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Data.Models
{
    public class TechnicalKnowledge : BaseModel<Guid>
    {
        public virtual ServiceModel Service { get; set; }

        [DisplayName("Análisis")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int ServiceId { get; set; }

        [DisplayName("Técnica analítica")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string AnalyticTechnique { get; set; }

        [Required(ErrorMessage = "La {0} es requerida.")]
        [DisplayName("Matriz")]
        public int MatrixId { get; set; }

        public virtual MatrixModel Matrix { get; set; }

        [DisplayName("Tiempo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Time { get; set; }

        public virtual Person Person { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid PersonId { get; set; }
    }
}