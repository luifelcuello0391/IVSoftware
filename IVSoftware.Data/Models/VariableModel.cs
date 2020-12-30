using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class VariableModel : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Unidad de medida")]
        public virtual MeasurementUnitModel MeasurementUnit { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string MeasurementUnitName { 
            get 
            {
                return MeasurementUnit != null && MeasurementUnit.Name != null &&
                !string.IsNullOrEmpty(MeasurementUnit.Name.Replace(" ", string.Empty)) ? MeasurementUnit.Name : "No aplica";
            }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedMeasurementUnitId
        {
            get;set;
        }

    }
}
