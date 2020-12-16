using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class TypeOfService : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
    }
}
