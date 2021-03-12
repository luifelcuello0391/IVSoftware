using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ServicesIntoQuotation
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public virtual ServiceModel Service { get; set; }
        public int? QuotationRequestId { get; set; }
        public virtual QuotationRequest QuotationRequest { get; set; }
        public int Quantity { get; set; }
        public float CurrentValue { get; set; }
        [NotMapped]
        public float TotalValue { get { return Quantity * CurrentValue; } }
    }
}
