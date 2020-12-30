using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    /// <summary>
    /// Es la clase base para todos los modelos
    /// </summary>
    public class BaseModelData
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; } // Nombre del registro
        [DisplayName("Estado del registro")]
        public int RegisterStatus { get; set; } // Indica el estado actual del registro (borrado lógico)
        [DisplayName("Fecha de creación")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDatetime { get; set; } = DateTime.Now; // Fecha y hora en la que fué creado el registro
        [DisplayName("Fecha de modificación")]
        [DataType(DataType.DateTime)]
        public DateTime? ModificationDatetime { get; set; } = DateTime.Now; // Fecha y hora de la última modificación del registro
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string RegisterStatusName {get{ return RegisterStatus > 0 ? "Activo" : "Inactivo"; } }

        public string GetYesNo(bool value)
        {
            return value ? "Sí" : "No";
        }
    }
}
