using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class Periodicity : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }
}