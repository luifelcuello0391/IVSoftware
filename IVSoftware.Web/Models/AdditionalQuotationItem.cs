using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class AdditionalQuotationItem : BaseModel
    {
        public int Id { get; set; }
        public float ItemValue { get; set; }
        public bool IsPercentage { get; set; }
        public bool AlwaysIncluded { get; set; }
    }
}
