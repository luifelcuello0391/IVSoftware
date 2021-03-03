using IVSoftware.Data.Models;
using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ServiceModel : BaseModelData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Código")]
        public int Code { get; set; }

        [DisplayName("Observaciones particulares de la prestación del servicio")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
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

        // Data added after presentation reunion
        [DisplayName("Acreditado por el IDEAM")]
        public bool AcredditedByIdeam { get; set; }
        [DisplayName("Autorizado INS - Min. salud")]
        public bool AuthorizedByINS { get; set; }
        [DisplayName("Tiempo de entrega de informe (días hábiles)")]
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

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Capacidad de recepción de muestras semanal (promedio)")]
        public int WeeklyAssignmentQuantity { get; set; } = 1;

        public Guid? PersonId { get; set; }
        [DisplayName("Analista responsable")]

        public virtual Person Responsable { get; set; }

        // New data aded after february 5th reunion
        public Guid? BackupAnalystId { get; set; }

        [DisplayName("Analista suplente")]
        public virtual Person BackupAnalyst { get; set; } // OK

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Capacidad de recepción de muestras semanal (máximo)")]
        public int MaximumWeeklyAssignment { get; set; } = 1; // OK

        [NotMapped]
        public int SelectedAnalisysTechniqueId { get; set; }

        [DisplayName("Técnica de análisis")]
        public virtual AnalisysTechnique AnalisysTechnique { get; set; }

        [NotMapped]
        public int SelectedAnalisysTypeId { get; set; }

        [DisplayName("Tipo de análisis (Área)")]
        public virtual AnalisysType AnalisysType { get; set; } // OK - falta hacer modal

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedTypeOfSamplingId { get; set; }

        [DisplayName("Tipo de muestreo")]
        public virtual SamplingType TypeOfSampling { get; set; } // OK - falta hacer modal

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Volumen establecido por el laboratorio (mL, agua, gr suelo)")]
        public double VolumeEstablishedByLaboratory { get; set; } // OK

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedStorageContainerId { get; set; }

        [DisplayName("Recipiente de almacenamiento")]
        public virtual StorageContainer StorageContainer { get; set; } // OK - falta hacer modal

        [DisplayName("Lavado y condiciones de limpieza para recipiente")]
        public string WashingCleaningConditionsForContainer { get; set; } // OK

        [DisplayName("Descripción de toma de muestras")]
        public string SamplingCaptureDescription { get; set; } // OK

        [DisplayName("Sustancias preservantes su concentración y cantidad")]
        public string PreservativesTheirConcentrationQuantity { get; set; } // OK

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Temperatura de almacenamiento")]
        public string StorageTemperature { get; set; } // OK

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Analizar antes de (días)")]
        public double AnalizeBefore { get; set; } // OK

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Tiempo regulatorio de almacenamiento EPA (días)")]
        public double RegulatoryStorageTime { get; set; } // OK

        [NotMapped]
        public bool GetReceptionOnMonday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("M;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool GetReceptionOnTuesday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("Tu;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool GetReceptionOnWednesday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("W;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool GetReceptionOnThursday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("Th;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool GetReceptionOnFriday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("F;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool GetReceptionOnSaturday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("S;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool GetReceptionOnSunday
        {
            get
            {
                if (!string.IsNullOrEmpty(ReceptionDays))
                {
                    return ReceptionDays.Contains("Su;");
                }

                return true;
            }
        }

        [NotMapped]
        public bool ReceptionOnMonday { get; set; } 

        [NotMapped]
        public bool ReceptionOnTuesday { get; set; } 

        [NotMapped]
        public bool ReceptionOnWednesday { get; set; }

        [NotMapped]
        public bool ReceptionOnThursday { get; set; } 

        [NotMapped]
        public bool ReceptionOnFriday { get; set; } 

        [NotMapped]
        public bool ReceptionOnSaturday { get; set; } 

        [NotMapped]
        public bool ReceptionOnSunday { get; set; } 

        [NotMapped]
        public string ObtainsReceptionDays
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                if (ReceptionOnMonday) builder.Append("M;");
                if (ReceptionOnTuesday) builder.Append("Tu;");
                if (ReceptionOnWednesday) builder.Append("W;");
                if (ReceptionOnThursday) builder.Append("Th;");
                if (ReceptionOnFriday) builder.Append("F;");
                if (ReceptionOnSaturday) builder.Append("S;");
                if (ReceptionOnSunday) builder.Append("Su;");

                return builder.ToString();
            }
        }

        [NotMapped]
        public string ObtainsReceptionDaysName
        {
            get
            {
                if(!string.IsNullOrEmpty(ReceptionDays))
                {
                    string data = ReceptionDays.Replace(";", ", ");
                    data = data.Replace("M", "Lunes");
                    data = data.Replace("Tu", "Martes");
                    data = data.Replace("W", "Miércoles");
                    data = data.Replace("Th", "Jueves");
                    data = data.Replace("F", "Viernes");
                    data = data.Replace("S,", "Sábado,");
                    data = data.Replace("Su", "Domingo");
                    data = data.Remove(data.Length - 2);

                    return data;
                }
                else
                {
                    return "No hay días de recepción";
                }
            }
        }

        [DisplayName("Observaciones")]
        public string AnalisysObservations { get; set; }

        [DisplayName("Días de recepción y condiciones particulares")]
        public string ReceptionDays { get; set; } // OK

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Tiempos de retención de ítem de ensayo (días)")]
        public double TestItemRetentionTimeInDays { get; set; } // OK

        [DisplayName("Protocolo relacionado")]
        public string RelatedProtocol { get; set; } // OK

        [NotMapped]
        public int SelectedUnitId { get; set; }

        [DisplayName("Unidades")]
        public virtual MeasurementUnitModel MeasurementUnit { get; set; }

        public virtual IEnumerable<TechnicalKnowledge> TechnicalKnowledges { get; set; }


        #region Data for working range
        [DisplayName("Precondición")]
        public string Precondition { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Valor mínimo")]
        public float MinimumValue { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
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

        [DisplayName("Límite de cuantificación")]
        public string QuantificationLimit { get; set; }
    }
}
