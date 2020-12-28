using IVSoftware.Data.Models.Core;
using System.Collections.Generic;

namespace IVSoftware.Data.Models
{
    public class AcademicLevel : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<HigherEducation> HigherEducations { get; set; }
    }
}