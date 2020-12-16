using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class ServiceGroupServicesRelation
    {
        public int Id { get; set; }
        public int? ServiceGroupId { get; set; }
        public virtual ServiceGroupModel ServiceGroup { get; set; }
        public int? ServiceId { get; set; }
        public virtual ServiceModel Service { get; set; }
    }
}
