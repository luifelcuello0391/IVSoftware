using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class CertificationType : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }
}