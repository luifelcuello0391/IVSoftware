using IVSoftware.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class QuotationRequest : BaseModel
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        [DisplayName("Cliente")]
        public virtual ClientModel Client { get; set; }
        public bool HasBeenGenerated { get; set; }
        [DisplayName("Fecha de última generación")]
        public DateTime? LastGenerationDate { get; set; }
        [DisplayName("Atendido por")]
        public virtual Persona GenerationUsed { get; set; }
        [DisplayName("Solicitud del cliente")]
        public string ClientRequestDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de solicitud")]
        public DateTime RequestDateTime { get; set; }
        [DisplayName("Estado")]
        public virtual QuotationStatus Status { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string StatusName { get { return Status != null && Status.Name != null && !string.IsNullOrEmpty(Status.Name.Replace(" ", string.Empty)) ? Status.Name : "Sin estado"; } }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [DisplayName("Nombre del cliente")]
        public string ClientName { get { return Client != null && Client.Name != null && !string.IsNullOrEmpty(Client.Name.Replace(" ", string.Empty)) ? Client.Name : "No asignado"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string RequestedClientName { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string RequestedClientId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [DisplayName("Identificación del cliente")]
        public string ClientIdentification { get { return Client != null && Client.Identification != null && !string.IsNullOrEmpty(Client.Identification.Replace(" ", string.Empty)) ? Client.Identification : "No especificado"; } }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string LastGenerationDateString { get { return LastGenerationDate != null && HasBeenGenerated ? LastGenerationDate.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : "---"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool HasBeenCanceled { get { return Status != null ? Status.Id == 4 : false; } }
        public virtual ICollection<ServicesIntoQuotation> Services { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int ManageQuotation { get; set; }
    }
}
