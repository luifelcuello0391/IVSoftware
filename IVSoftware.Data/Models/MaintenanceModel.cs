using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class MaintenanceModel : BaseModelData
    {
        public int Id { get; set; }

        [DisplayName("Tipo de mantenimiento")]
        public string TypeOfMaintenanceId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string TypeOfMaintenance
        {
            get
            {
                switch (TypeOfMaintenanceId)
                {
                    case "M":
                        return "Mantenimiento";

                    case "C":
                        return "Calibración";

                    default:
                        return "No definido";
                }
            }
        }

        public int EquipId { get; set; }

        [DisplayName("Equipo")]
        public virtual Equipment Equip { get; set; }

        [DisplayName("Fecha de mantenimiento")]
        public DateTime MaintenanceDate { get; set; }

        [DisplayName("Falla encontrada")]
        public string Diagnostic { get; set; }

        [DisplayName("Trabajo efectuado")]
        public string WorkExecuted { get; set; }

        [DisplayName("Fecha de la próxima calibración")]
        public DateTime? NextCalibrationDate { get; set; }

        [DisplayName("Observaciones")]
        public string Observations { get; set; }

        public int? ProviderId { get; set; }

        [DisplayName("Proveedor")]
        public virtual Provider ServiceProvider { get; set; }

        public Guid? PersonId { get; set; }
        [DisplayName("Responsable")]
        public virtual Person Responsable { get; set; }

        public Guid? MaintenanceCertificateDocumentId { get; set; }
        public virtual Document MaintenanceCertificateDocument { get; set; }
    }
}
