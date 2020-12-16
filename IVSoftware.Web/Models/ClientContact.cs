using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class ClientContact : BaseModel
    {
        public int Id { get; set; }
        public virtual TipoDocumento DocumentType { get; set; }
        public string Identification { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public virtual Departamento AddressDepartment { get; set; }
        public virtual Municipio City { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public string CellPhone { get; set; }
        public string ReportDeliveryEmail { get; set; }
    }
}
