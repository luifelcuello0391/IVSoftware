using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class ReferenceMethodCondition : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Código de la cotización")]
        public string QuotationCode { get; set; }
    }
}
