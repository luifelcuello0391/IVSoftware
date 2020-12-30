using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class CheckListSection : BaseModelData
    {
        public CheckListSection()
        {
        }
        public int Id { get; set; }
        [DisplayName("Listado de preguntas")]
        public virtual ICollection<CheckListQuestionCheckListSection> QuestionSections { get; set; }
        [DisplayName("Listas de chequeo")]
        public virtual ICollection<EquipmentCheckListSections> CheckLists { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int QuestionsCount { get { return QuestionSections != null ? QuestionSections.Count : 0; } }
    }
}
