using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class Eps : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}