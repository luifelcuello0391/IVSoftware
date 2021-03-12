using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ClientContact : BaseModelData
    {
        public int Id { get; set; }
        public virtual IdentificationType DocumentType { get; set; }
        public string Identification { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public virtual Department AddressDepartment { get; set; }
        public virtual Municipality City { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public string CellPhone { get; set; }
        [DisplayName("Correo para el envío de confirmación")]
        public string ReportDeliveryEmail { get; set; }
    }
}
