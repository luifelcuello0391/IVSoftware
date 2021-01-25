using IVSoftware.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using IVSoftware.Models;

namespace IVSoftware.Data.Models
{
    public class MaintenanceModel: BaseModelData 
    {
        public int Id { get; set; }
        [DisplayName("Tipo de mantenimiento")]
        public string TypeOfMaintenanceId { get; set; }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string TypeOfMaintenance { 
            get { 
                switch(TypeOfMaintenanceId)
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

        //[DisplayName("Estado del registro")]
        //public int RegisterStatus { get; set; } // Indica el estado actual del registro (borrado lógico)
        
        //[DisplayName("Fecha de creación")]
        //[DataType(DataType.DateTime)]
        //public DateTime CreationDatetime { get; set; } = DateTime.Now; // Fecha y hora en la que fué creado el registro
        
        //[DisplayName("Fecha de modificación")]
        //[DataType(DataType.DateTime)]
        //public DateTime? ModificationDatetime { get; set; } = DateTime.Now; // Fecha y hora de la última modificación del registro
        
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public string RegisterStatusName { get { return RegisterStatus > 0 ? "Activo" : "Inactivo"; } }
    }
}
