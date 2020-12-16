using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class MatrixModel : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("¿Sólo para análisis en laboratorio?")]
        public bool OnlyForServices { get; set; } = true;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string OnlyForServicesText { get { return OnlyForServices ? "Sí" : "No"; } }
    }
}
