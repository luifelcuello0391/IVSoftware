using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class IncentivesIntoServiceQuotationRequest
    {
        public int Id { get; set; }
        public int? QuotationRequestId { get; set; }
        public virtual QuotationRequest QuotationRequest { get; set; }

        public int? IncentiveId { get; set; }
        public virtual IncentiveModel Incentive { get; set; }

        public float IncentiveCurrentValue { get; set; }

        public bool IsPercentage { get; set; }

        [NotMapped]
        public float ServiceTotalValue { get; set; }

        [NotMapped]
        public float IncentiveValueFromTotal
        {
            get
            {
                return ServiceTotalValue * (IncentiveCurrentValue / 100f);
            }
        }
    }
}
