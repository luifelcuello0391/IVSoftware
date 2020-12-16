using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class QuotationServiceModel : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Valor ")]
        public float ServiceValue { get; set; }
    }
}
