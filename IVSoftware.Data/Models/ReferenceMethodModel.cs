using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class ReferenceMethodModel : BaseModelData
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Valor inferior de la incertidumbre")]
        public float MeasurementUncertaintyLowerValue { get; set; }
        [DisplayName("Valor superior de la incertidumbre")]
        public float MeasurementUncertaintyUpperValue { get; set; }
        [DisplayName("La incertidumbre es un porcentaje")]
        public bool IsPercentage { get; set; } = false;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string IsPercentageValue { get { return IsPercentage ? "Sí" : "No"; } }
        [DisplayName("Aplica incertidumbre")]
        public bool HasUncertainty { get; set; } = true;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string HasUncertaintyValue { get { return HasUncertainty ? "Sí" : "No"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Uncertainty
        {
            get
            {
                try
                {
                    if (HasUncertainty)
                    {
                        if (IsPercentage)
                        {
                            return string.Format("{0:0.##}%", MeasurementUncertaintyLowerValue);
                        }
                        else // ± a value
                        {
                            StringBuilder result = new StringBuilder();
                            result.Append("Entre ±");
                            result.Append(string.Format("[{0}]",
                                (MeasurementUncertaintyLowerValue == MeasurementUncertaintyUpperValue) ?
                                string.Format("{0:0.###}", MeasurementUncertaintyLowerValue) :
                                string.Format("{0:0.###} a {1:0.###}", MeasurementUncertaintyLowerValue, MeasurementUncertaintyUpperValue)));

                            return result.ToString();
                        }
                    }
                    else
                    {
                        return "No aplica";
                    }
                }
                catch (Exception ex)
                {
                    return "Error";
                }
            }            
        }

    }
}
