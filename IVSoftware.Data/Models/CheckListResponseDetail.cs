using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class CheckListResponseDetail : BaseModelData
    {
        public int Id { get; set; }
        public virtual ChecklistResponseHeader Header { get; set; }
        public virtual CheckListQuestion Question { get; set; }
        public string Response { get; set; }
    }
}
