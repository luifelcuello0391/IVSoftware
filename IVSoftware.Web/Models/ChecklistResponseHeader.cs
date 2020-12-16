using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class ChecklistResponseHeader : BaseModel
    {
        public int Id { get; set; }
        public int? EquipmentId { get; set; }
        [DisplayName("Equipo")]
        public virtual Equipment Equipment { get; set; }
        public int? CheckListId { get; set; }
        [DisplayName("Lista de chequeo")]
        public virtual EquipmentCheckList CheckList { get; set; }
        [DisplayName("Fecha de diligenciamiento")]
        public virtual DateTime FillUpDate { get; set; }
        [DisplayName("NOTA: EN CASO DE CALIBRACIÓN INDICAR LOS PATRONES CON LOS CUALES SE REALIZÓ Y SU TRAZABILIDAD:")]
        public string Observation { get; set; }
        [DisplayName("Resultado de la aptitud")]
        public bool ValidationResult { get; set; }
        [NotMapped]
        public string ValidationResultName { get { return ValidationResult ? "Conforme" : "No conforme"; } }
        [DisplayName("Revisó")]
        public virtual Persona ValidatedBy { get; set; }
        public virtual ICollection<CheckListResponseDetail> Details { get; set; }

    }
}
