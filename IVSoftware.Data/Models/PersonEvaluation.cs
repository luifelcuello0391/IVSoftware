using System;

namespace IVSoftware.Data.Models
{
    public class PersonEvaluation
    {
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        public int EvaluationId { get; set; }

        public virtual Evaluation Evaluation { get; set; }
    }
}