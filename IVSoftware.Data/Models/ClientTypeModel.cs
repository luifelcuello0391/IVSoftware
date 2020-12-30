using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    /// <summary>
    /// Tipos de cliente 
    /// </summary>
    public class ClientTypeModel : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("¿Debe pagar por los servicios?")]
        public bool MustPayServices { get; set; } = true; // Indica si el tipo de cliente debe o no pagar por los servicios
        public string MustPayServicesText { get { return MustPayServices ? "Sí" : "No"; } }
        public virtual ICollection<ClientTypeIncentiveRelation> Incentives { get; set; }
        [DisplayName("¿Tiene incentivos?")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string HaveIncentive { get { return Incentives != null && Incentives.Count > 0 ? "Sí" : "No"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int AssignedIncentivesCount { get { return Incentives != null ? Incentives.Count : 0; } }
    }
}
