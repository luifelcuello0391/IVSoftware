using IVSoftware.Data.Models.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace IVSoftware.Data.Models
{
    public class CertificationType : BaseModel<int>
    {
        [DisplayName("Tipo de Certificación")]
        public string Name { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }
}