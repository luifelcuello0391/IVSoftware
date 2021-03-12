using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class QuotationRequest : BaseModelData
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        [DisplayName("Cliente")]
        public virtual ClientModel Client { get; set; }
        public bool HasBeenGenerated { get; set; }
        [DisplayName("Fecha de última generación")]
        public DateTime? LastGenerationDate { get; set; }
        [DisplayName("Atendido por")]
        public virtual Person GenerationUsed { get; set; }
        [DisplayName("Comentarios generales")]
        public string ClientRequestDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Debe ingresar la fecha de solicitud")]
        [DisplayName("Fecha de solicitud")]
        public DateTime RequestDateTime { get; set; }
        [DisplayName("Estado")]
        public virtual QuotationStatus Status { get; set; }
        [NotMapped]
        public string StatusName { get { return Status != null && Status.Name != null && !string.IsNullOrEmpty(Status.Name.Replace(" ", string.Empty)) ? Status.Name : "Sin estado"; } }
        
        [NotMapped]
        [DisplayName("Nombre del cliente")]
        public string ClientName { get { return Client != null && Client.Name != null && !string.IsNullOrEmpty(Client.Name.Replace(" ", string.Empty)) ? Client.Name : "No asignado"; } }
        [NotMapped]
        [DisplayName("Cliente que cotiza")]
        public string RequestedClientName { get; set; }

        [NotMapped]
        public string RequestedClientId { get; set; }

        [NotMapped]
        [DisplayName("Identificación del cliente")]
        public string ClientIdentification { get { return Client != null && Client.Identification != null && !string.IsNullOrEmpty(Client.Identification.Replace(" ", string.Empty)) ? Client.Identification : "No especificado"; } }

        [NotMapped]
        public string LastGenerationDateString { get { return LastGenerationDate != null && HasBeenGenerated ? LastGenerationDate.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : "---"; } }
        [NotMapped]
        public bool HasBeenCanceled { get { return Status != null ? Status.Id == 4 : false; } }
        public virtual ICollection<ServicesIntoQuotation> Services { get; set; }
        [NotMapped]
        public int ManageQuotation { get; set; }

        public int ServicesReportTime
        {
            get; set;
        }

        public float ServicesTotal
        {
            get; set;
        }

        public virtual ICollection<IncentivesIntoServiceQuotationRequest> Incentives { get; set; }

        public int? ContactId { get; set; }

        public virtual ClientContact Contact { get; set; }

        public float QuotationTotalValue { get; set; }

        public virtual ICollection<TaxesIntoServiceQuotationRequest> Taxes { get; set; }
        public float QuotationTotalValueAfterTaxes { get; set; }
        public string CancelationResponse { get; set; }
    }
}
