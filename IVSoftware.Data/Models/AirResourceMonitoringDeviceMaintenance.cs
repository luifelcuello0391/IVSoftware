using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IVSoftware.Data.Models
{
    public class AirResourceMonitoringDeviceMaintenance : BaseModelData
    {
        public int Id { get; set; }

        [DisplayName("Tipo")]
        public string TypeOfMaintenanceId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string TypeOfMaintenance
        {
            get
            {
                switch (TypeOfMaintenanceId)
                {
                    case "R":
                        return "Mantenimiento correctivo (reparación)";

                    case "P":
                        return "Mantenimiento preventivo";

                    case "C":
                        return "Calibración";

                    default:
                        return "No definido";
                }
            }
        }
        public int EquipId { get; set; }

        [DisplayName("Equipo")]
        public virtual Equipment Equipment { get; set; }

        [DisplayName("Fecha de mantenimiento")]
        public DateTime MaintenanceDate { get; set; }

        #region Provider data
        [DisplayName("Nombre")]
        public string ProviderName { get; set; }
        [DisplayName("Nit")]
        public string ProviderIdentificaton { get; set; }
        [DisplayName("Dirección")]
        public string ProviderAddress { get; set; }
        [DisplayName("Teléfono")]
        public string ProviderPhoneNumber { get; set; }
        [DisplayName("Nombre del contacto")]
        public string ProviderContactName { get; set; }
        #endregion

        [DisplayName("Respuestos cambiados")]
        public string SparePartsChanged { get; set; }
        [DisplayName("Próximo mantenimiento")]
        public DateTime NextMaintenanceDate { get; set; }
        [DisplayName("Observaciones")]
        public string Observations { get; set; }
        [DisplayName("Ubicación")]
        public string Location { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Número de inventario")]
        public int StockNumber { get; set; }
        public Guid? PersonId { get; set; }
        [DisplayName("Responsable")]
        public virtual Person Responsable { get; set; }
    }
}
