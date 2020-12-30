using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class Provider : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("Identificación")]
        public string Identification { get; set; }
        [DisplayName("Dirección")]
        public string Address { get; set; }
        [DisplayName("Teléfono")]
        public string PhoneNumber { get; set; }
        [DisplayName("Correo electrónico")]
        public string Email { get; set; }
        [DisplayName("Contacto")]
        public string Contact { get; set; }
    }
}
