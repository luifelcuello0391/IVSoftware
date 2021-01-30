using System;

namespace IVSoftware.Web.Data
{
    public class AnswerVM
    {
        public Guid Id { get; set; }

        public string Answer { get; set; }

        public bool IsRight { get; set; }

        public bool IsSelected { get; set; }
    }
}