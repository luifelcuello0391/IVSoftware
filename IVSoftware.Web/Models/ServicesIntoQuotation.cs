﻿using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class ServicesIntoQuotation
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public virtual ServiceModel Service { get; set; }
        public int? QuotationRequestId { get; set; }
        public virtual QuotationRequest QuotationRequest { get; set; }
    }
}
