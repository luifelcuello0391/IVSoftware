using IVSoftware.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class ServiceModel : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        public int Code { get; set; }
        [DisplayName("Observaciones particulares de la prestación del servicio")]
        public string Description { get; set; }
        [DisplayName("Valor unitario")]
        public float UnitValue { get; set; }
        [DisplayName("Tipo de servicio")]
        public virtual TypeOfService ServiceType { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ServiceTypeName { get { return ServiceType != null && ServiceType.Name != null && !string.IsNullOrEmpty(ServiceType.Name.Replace(" ", string.Empty)) ? ServiceType.Name : "No definido"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int TypeOfServiceId { get; set; }
        [DisplayName("Matriz")]
        public virtual MatrixModel MatrixGroup { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string MatrixGroupName { get { return MatrixGroup != null && MatrixGroup.Name != null && !string.IsNullOrEmpty(MatrixGroup.Name.Replace(" ", string.Empty)) ? MatrixGroup.Name : "No definido"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedMatrixGroupId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedVariableId { get; set; }
        [DisplayName("Método de referencia")]
        public virtual ReferenceMethodModel ReferenceMethod { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ReferenceMethodName { get { return ReferenceMethod != null && ReferenceMethod.Name != null && !string.IsNullOrEmpty(ReferenceMethod.Name.Replace(" ", string.Empty)) ? ReferenceMethod.Name : "No definido"; }}
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedReferenceMethodId { get; set; }
        [DisplayName("Rango de trabajo")]
        public virtual WorkingRangeModel WorkingRange { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string WorkingRangeName { get { return WorkingRange != null && WorkingRange.Name != null && !string.IsNullOrEmpty(WorkingRange.Name.Replace(" ", string.Empty)) ? WorkingRange.Name : "No definido"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedWorkingRangeId { get; set; }

        // Data added after presentation reunion
        [DisplayName("Acreditado por el IDEAM")]
        public bool AcredditedByIdeam { get; set; }
        [DisplayName("Autorizado INS - Min. salud")]
        public bool AuthorizedByINS { get; set; }
        [DisplayName("Tiempo de entrega de informe (días)")]
        public int ReportDeliveryTime { get; set; }
        [DisplayName("Vigente")]
        public bool Valid { get; set; }
        [DisplayName("Código de facturación")]
        public string BillingCode { get; set; }
        [DisplayName("Nombre de facturación")]
        public string BillingName { get; set; }

        public virtual ICollection<ServiceGroupModel> ServiceGroups { get; set; }

        public string GetYesNo (bool value)
        {
            return value ? "Sí" : "No";
        }
    }
}
