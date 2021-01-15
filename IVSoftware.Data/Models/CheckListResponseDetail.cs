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
        public int? SectionId { get; set; }
        public virtual CheckListSection Section { get; set; } 
        public int HeaderId { get; set; }
        public virtual ChecklistResponseHeader Header { get; set; }
        public int QuestionId { get; set; }
        public virtual CheckListQuestion Question { get; set; }
        public string Response { get; set; }
    }
}
