using System;
using System.Collections.Generic;

namespace IVSoftware.Web.Data
{
    public class EvaluationVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public ICollection<QuestionVM> Questions { get; set; }
    }
}