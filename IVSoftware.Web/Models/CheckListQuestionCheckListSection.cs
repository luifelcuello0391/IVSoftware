using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class CheckListQuestionCheckListSection
    {
        public int CheckListQuestionCheckListSectionId { get; set; }

        public int? QuestionsId { get; set; }
        public virtual CheckListQuestion CheckListQuestion { get; set; }

        public int? SectionsId { get; set; }
        public virtual CheckListSection CheckListSection { get; set; }
        public int Order { get; set; }
    }
}
