using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class CheckListQuestion : BaseModel
    {
        public CheckListQuestion()
        {
            //this.CheckList = new List<EquipmentCheckList>();
            //this.Sections = new List<CheckListSection>();
        }

        public int Id { get; set; }
        [DisplayName("Tipo de pregunta")]
        public int TypeOfQuestion { get; set; } = 1;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string TypeOfQuestionName { get 
            {
                switch(this.TypeOfQuestion)
                {
                    case 1: // Sí-No-N/A
                    return "Sí-No-N/A";

                    case 2: // Texto
                    return "Texto";

                    default:
                        return "No especificado";
                }
            } }

        //public virtual ICollection<CheckListSection> Sections { get; set; }
        public virtual ICollection<CheckListQuestionCheckListSection> QuestionSection { get; set; }
        public virtual ICollection<EquipmentCheckListQuestions> CheckList { get; set; }
    }
}
