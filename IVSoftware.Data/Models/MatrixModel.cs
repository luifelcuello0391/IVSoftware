using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class MatrixModel : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("¿Sólo para análisis en laboratorio?")]
        public bool OnlyForServices { get; set; } = true;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string OnlyForServicesText { get { return OnlyForServices ? "Sí" : "No"; } }

        public virtual ICollection<TechnicalKnowledge> TechnicalKnowledges { get; set; }
    }
}
