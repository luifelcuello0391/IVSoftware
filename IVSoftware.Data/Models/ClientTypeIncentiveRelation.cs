using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ClientTypeIncentiveRelation
    {
        public int Id { get; set; }
        public int? ClientTypeId { get; set; }
        public virtual ClientTypeModel ClientType { get; set; }
        public int? IncentiveId { get; set; }
        public virtual IncentiveModel Incentive { get; set; }
    }
}
