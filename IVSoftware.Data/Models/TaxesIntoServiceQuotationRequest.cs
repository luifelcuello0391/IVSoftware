using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class TaxesIntoServiceQuotationRequest
    {
        public int Id { get; set; }

        public int? TaxId { get; set; }
        public virtual TaxModel Tax { get; set; }
        public int? QuotationRequestId { get; set; }
        public virtual QuotationRequest QuotationRequest { get; set; }
        public float CurrentTaxValue { get; set; }
        [NotMapped]
        public float QuotationTotal { get; set; }
        public float ValueFromQuotationTotal { 
            get
            {
                return QuotationTotal * (CurrentTaxValue/100f);
            }
        }
    }
}
