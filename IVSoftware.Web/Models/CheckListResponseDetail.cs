using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class CheckListResponseDetail : BaseModel
    {
        public int Id { get; set; }
        public virtual ChecklistResponseHeader Header { get; set; }
        public virtual CheckListQuestion Question { get; set; }
        public string Response { get; set; }
    }
}
