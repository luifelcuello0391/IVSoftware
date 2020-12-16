using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class EquipmentCheckListQuestions
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public virtual CheckListQuestion Question { get; set; }
        public int? CheckListId { get; set; }
        public virtual EquipmentCheckList Checklist { get; set; }
        public int Order { get; set; }
    }
}
