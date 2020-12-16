using IVSoftware.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    /// <summary>
    /// Contiene toda la información de los clientes
    /// </summary>
    public class ClientModel : BaseModel
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="Este campo es obligatorio")]
        [DisplayName("Tipo de documento")]
        public virtual TipoDocumento  DocumentType { get; set; } // Código del tipo de documento
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DocumentTypeName { get { return DocumentType != null && DocumentType.Nombre != null && !string.IsNullOrEmpty(DocumentType.Nombre.Replace(" ", string.Empty)) ? DocumentType.Nombre : "No definido"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedDocumentTypeId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Identificación")]
        public string Identification { get; set; } // Identificación del cliente
        [DisplayName("Dirección")]
        public string Address { get; set; } // Dirección dónde reside el cliente
        [DisplayName("Teléfono fijo")]
        public string PhoneNumber { get; set; } // Teléfonos de contacto del cliente
        [DisplayName("Nombre de contacto principal")]
        public string ContactName { get; set; } // Nombre de la persona de contacto
        [DisplayName("Correo electrónico para entrega de facturación")]
        public string EmailAddress { get; set; } // Dirección de correo electrónico del cliente
        [DisplayName("Tipo de cliente")]
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public virtual ClientTypeModel ClientType { get; set; } = null; // Tipo de cliente
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ClientTypeName { get { return ClientType != null && ClientType.Name != null && !string.IsNullOrEmpty(ClientType.Name.Replace(" ", string.Empty)) ? ClientType.Name : "No definido"; } }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedClientTypeId { get; set; }

        [DisplayName("¿Es habitual?")]
        public bool IsRegular { get; set; } = false; // Indica si es un cliente habitual o no
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string IsRegularText { get { return IsRegular ? "Sí" : "No"; } }
        [DisplayName("Fecha de ejecución del último servicio")]
        public DateTime LastServiceExecutionDatetime { get; set; } // Especifica la fecha del último servicio realizado

        // Data included after presentation reunion
        [DisplayName("Tipo de persona")]
        public int PersonType { get; set; } = 1; // 1 = Natural, 2 = Legal
        [DisplayName("Extensión")]
        public string Extension { get; set; }
        [DisplayName("Departamento")]
        public virtual Departamento Department { get; set; }
        public int? DepartmentId { get; set; }
        [DisplayName("Ciudad")]
        public virtual Municipio City { get; set; }
        public int? CityId { get; set; }
        [DisplayName("Teléfono celular")]
        public string CellPhone { get; set; }
        public virtual ICollection<ClientContact> Contacts { get; set; }
    }
}
