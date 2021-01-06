using IVSoftware.Data.Models;
using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ServiceModel : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        public int Code { get; set; }
        [DisplayName("Observaciones particulares de la prestación del servicio")]
        public string Description { get; set; }
        [DisplayName("Valor unitario")]
        public float UnitValue { get; set; }
        public int? ServiceTypeId { get; set; }
        [DisplayName("Tipo de servicio")]
        public virtual TypeOfService ServiceType { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ServiceTypeName { get { return ServiceType != null && ServiceType.Name != null && !string.IsNullOrEmpty(ServiceType.Name.Replace(" ", string.Empty)) ? ServiceType.Name : "No definido"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int TypeOfServiceId { get; set; }
        public int? MatrixGroupId { get; set; }
        [DisplayName("Matriz")]
        public virtual MatrixModel MatrixGroup { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string MatrixGroupName { get { return MatrixGroup != null && MatrixGroup.Name != null && !string.IsNullOrEmpty(MatrixGroup.Name.Replace(" ", string.Empty)) ? MatrixGroup.Name : "No definido"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedMatrixGroupId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedVariableId { get; set; }
        public int? ReferenceMethodId { get; set; }
        [DisplayName("Método de referencia")]
        public virtual ReferenceMethodModel ReferenceMethod { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ReferenceMethodName { get { return ReferenceMethod != null && ReferenceMethod.Name != null && !string.IsNullOrEmpty(ReferenceMethod.Name.Replace(" ", string.Empty)) ? ReferenceMethod.Name : "No definido"; }}
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedReferenceMethodId { get; set; }

        //public int? WorkingRangeId { get; set; }
        //[DisplayName("Rango de trabajo")]
        //public virtual WorkingRangeModel WorkingRange { get; set; }
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public string WorkingRangeName { get { return WorkingRange != null && WorkingRange.Name != null && !string.IsNullOrEmpty(WorkingRange.Name.Replace(" ", string.Empty)) ? WorkingRange.Name : "No definido"; } }
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public int SelectedWorkingRangeId { get; set; }

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

        public virtual ICollection<ServiceGroupServicesRelation> ServiceGroups { get; set; }        

        // New data added after december 25th reunion

        [DisplayName("Disponible para portal de clientes")]
        public bool AvailableForClients { get; set; } = true;

        [DisplayName("Capacidad operativa semanal")]
        public int WeeklyAssignmentQuantity { get; set; } = 1;

        public Guid? PersonId { get; set; }
        [DisplayName("Responsable")]

        public virtual Person Responsable { get; set; }

        #region Data for working range
        [DisplayName("Precondición")]
        public string Precondition { get; set; }
        [DisplayName("Valor mínimo")]
        public float MinimumValue { get; set; }
        [DisplayName("Valor máximo")]
        public float MaximumValue { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string WorkingRange
        {
            get
            {
                try
                {
                    StringBuilder result = new StringBuilder();

                    result.Append(string.Format("{0:0.##} - {1:0.##}", MinimumValue, MaximumValue));

                    if (Precondition != null && !string.IsNullOrEmpty(Precondition.Replace(" ", string.Empty)))
                    {
                        result.Append(string.Format(" >> {0}", Precondition));
                    }

                    return result.ToString();
                }
                catch
                {
                    return "Error";
                }
            }

        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string WorkingRangeWithoutPrecondition
        {
            get
            {
                try
                {
                    return string.Format("{0:0.##} - {1:0.##}", MinimumValue, MaximumValue);
                }
                catch
                {
                    return "Error";
                }
            }

        }
        #endregion

        public virtual ICollection<TechnicalKnowledge> TechnicalKnowledges { get; set; }
    }
}
