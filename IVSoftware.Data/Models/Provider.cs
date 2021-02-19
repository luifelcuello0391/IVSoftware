using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IVSoftware.Data.Models
{
    public class Provider : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("NIT")]
        public string Identification { get; set; }
        [DisplayName("RUT")]
        public string Rut { get; set; }
        [DisplayName("Dirección")]
        public string Address { get; set; }
        [DisplayName("Teléfono(s)")]
        public string PhoneNumber { get; set; }
        [DisplayName("Correo electrónico")]
        public string Email { get; set; }
        [DisplayName("Nombre de persona de contacto")]
        public string Contact { get; set; }

        [DisplayName("Sitio web")]
        public string WebPage { get; set; }
        
        public Guid? RutDocuentId { get; set; }
        public virtual Document RutDocument { get; set; }

        [NotMapped]
        public string DocumentFile { get; set; }
    }
}
