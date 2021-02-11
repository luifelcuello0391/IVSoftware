using System;

namespace IVSoftware.Web.Data
{
    public class RescheduleVM
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}