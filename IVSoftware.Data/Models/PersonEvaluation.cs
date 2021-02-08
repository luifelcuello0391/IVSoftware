using IVSoftware.Data.Models.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVSoftware.Data.Models
{
    public class PersonEvaluation : BaseModel<Guid>
    {
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        public int EvaluationId { get; set; }

        public virtual Evaluation Evaluation { get; set; }

        public string ResultJson { get; set; }

        [DisplayName("Fecha de asignación")]
        public DateTime AssignedDate { get; set; }

        [DisplayName("Fecha de realización")]
        public DateTime? Date { get; set; }

        [DisplayName("Puntaje obtenido (%)")]
        public int? Score { get; set; }

        [DisplayName("Estado")]
        public bool? IsApproved { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}