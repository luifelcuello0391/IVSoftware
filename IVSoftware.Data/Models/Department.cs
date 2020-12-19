using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class Department : BaseModel<int>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public virtual ICollection<Municipality> Municipalities { get; set; }
    }
}