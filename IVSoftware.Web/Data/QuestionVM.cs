using System;
using System.Collections.Generic;

namespace IVSoftware.Web.Data
{
    public class QuestionVM
    {
        public Guid Id { get; set; }

        public string Question { get; set; }

        public ICollection<AnswerVM> Answers { get; set; }
    }
}