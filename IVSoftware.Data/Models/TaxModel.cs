using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class TaxModel : BaseModelData
    {
        public int Id { get; set; }
        public float Currentvalue { get; set; }
    }
}
