using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class EquipmentCheckList : BaseModel
    {
        public int Id { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<EquipmentCheckListSections> Sections { get; set; }
        [DisplayName("Preguntas")]
        public virtual ICollection<EquipmentCheckListQuestions> Questions { get; set; }

    }
}
