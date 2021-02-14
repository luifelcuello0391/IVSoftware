using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class Document : BaseModel<Guid>
    {
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("URL")]
        public string Url { get; set; }

        [DisplayName("Fecha de creación")]
        public DateTime CreationDatetime { get; set; }

        [DisplayName("Fecha de modificación")]
        public DateTime ModiicationDatetime { get; set; }
    }
}
