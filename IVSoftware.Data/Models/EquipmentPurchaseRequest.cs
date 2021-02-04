using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class EquipmentPurchaseRequest : BaseModelData
    {
        public int Id { get; set; }

        [DisplayName("Fecha de solicitud")]
        public DateTime RequestDate { get; set; }

        [DisplayName("Nombre del equipo")]
        public string EquipmentName { get; set; }

        [DisplayName("Parámetros (Método)")]
        public string Parameters { get; set; }

        [DisplayName("Área")]
        public string Location { get; set; }

        [DisplayName("Justificación")]
        public string Justification { get; set; }

        public Guid? AnalystPersonId { get; set; }

        [DisplayName("Analista responsable")]
        public virtual Person ResponsibleAnalyst { get; set; }

        [DisplayName("Rango de trabajo")]
        public string WorkRange { get; set; }

        [DisplayName("Voltaje")]
        public double Voltage { get; set; }

        [DisplayName("Exactitud")]
        public string Accuracy { get; set; }

        [DisplayName("Lectura mínima (Resolución)")]
        public string MinimumRead { get; set; }

        [DisplayName("Compatibilidaad para trabajar en Red")]
        public string NetworkCompatibility { get; set; }

        [DisplayName("Frecuencia")]
        public string Frequency { get; set; }

        [DisplayName("Conficiones ambientales")]
        public string EnvironmentConditions { get; set; }

        [DisplayName("Accesorios")]
        public string Accessories { get; set; }

        [DisplayName("Requiere entrenamiento (General o especializado)")]
        public bool RequiresLearning { get; set; }

        [DisplayName("Intensidad horaria")]
        public int HourlyIntensity { get; set; }

        [DisplayName("Garantía")]
        public string Warranty { get; set; }

        [DisplayName("Observaciones")]
        public string Observations { get; set; }

        [DisplayName("Estado de la solicitud")]
        public int RequestStatus { get; set; }

        [NotMapped]
        public string RequestStatusName
        {
            get
            {
                switch(RequestStatus)
                {
                    case 2:
                        return "Aprobado";

                    case 3:
                        return "Rechazado";

                    default:
                        return "Nueva";
                }
            }
        }

        [DisplayName("Fecha de aprobación/rechazo")]
        public DateTime? RequestStatusDate { get; set; }

        public Guid? ResponsePersonId { get; set; }

        [DisplayName("Quien aprueba/rechaza la solicitud")]
        public virtual Person StatusChangePerson { get; set; }

    }
}
