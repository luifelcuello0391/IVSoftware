using System;

namespace IVSoftware.Data.Models
{
    public class PersonJobRole
    {
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        public int JobRoleId { get; set; }

        public virtual JobRole JobRole { get; set; }
    }
}