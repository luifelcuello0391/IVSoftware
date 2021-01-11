using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class JobRole : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<PersonJobRole> PeopleJobRole { get; set; }
    }
}