using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class EquipmentCheckListSections
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public virtual CheckListSection Section { get; set; }
        public int? ChecklistId { get; set; }
        public virtual EquipmentCheckList CheckList { get; set; }
        public int Order { get; set; }
    }
}
