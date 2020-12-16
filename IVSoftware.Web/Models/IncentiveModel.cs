using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class IncentiveModel : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Valor")]
        public float Value { get; set; }
        [DisplayName("¿Es un porcentaje?")]
        public bool IsPercentage { get; set; }
        public virtual ICollection<ClientTypeIncentiveRelation> ClientTypes { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string IsPercentageValue { get { return IsPercentage ? "Sí" : "No"; } }
        public string ValueText { get { return IsPercentage ? string.Format("{0} %", Value) : string.Format("$ {0}", Value); } }
    }
}
