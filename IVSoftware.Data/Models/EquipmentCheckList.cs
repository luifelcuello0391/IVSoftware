using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class EquipmentCheckList : BaseModelData
    {
        public int Id { get; set; }

        [DisplayName("Secciones")]
        public virtual ICollection<EquipmentCheckListSections> Sections { get; set; }
        [DisplayName("Preguntas")]
        public virtual ICollection<EquipmentCheckListQuestions> Questions { get; set; }

    }
}
