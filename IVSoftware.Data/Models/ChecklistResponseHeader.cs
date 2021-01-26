using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ChecklistResponseHeader : BaseModelData
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
        
        public Guid? ValidatedById { get; set; }

        [DisplayName("Revisó")]
        public virtual Person ValidatedBy { get; set; }
        public virtual ICollection<CheckListResponseDetail> Details { get; set; }

        public string GetResponse (int? sectionId, int questionId, int questionType)
        {
            string response = "N/A";

            if(Details != null && Details.Count > 0)
            {
                try
                {
                    CheckListResponseDetail detail = null;

                    if (sectionId != null)
                    {
                        detail = Details.Where<CheckListResponseDetail>(x => x.SectionId == sectionId && x.QuestionId == questionId).FirstOrDefault();
                    }
                    else
                    {
                        detail = Details.Where<CheckListResponseDetail>(x => x.SectionId == null && x.QuestionId == questionId).FirstOrDefault();
                    }                    
                    
                    if(detail != null)
                    {
                        response = detail.Response;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error on GetResponse >> " + ex.ToString());
                }
            }

            switch(questionType)
            {
                case 1: // Si-NO-N/A
                    switch(response.Trim())
                    {
                        case "1":
                            return "Si";

                        case "2":
                            return "No";

                        default:
                            return "N/A";
                    }

                default:
                    if(response == null || (response.Equals("N/A")))
                    {
                        return "Sin respuesta";
                    }
                    else
                    {
                        return response;
                    }
            }
        }

    }
}
