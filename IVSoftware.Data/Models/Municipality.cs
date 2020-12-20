using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class Municipality : BaseModel<int>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Person> PeopleCorrespondence { get; set; }

        public virtual ICollection<Person> PeopleBirth { get; set; }
    }
}