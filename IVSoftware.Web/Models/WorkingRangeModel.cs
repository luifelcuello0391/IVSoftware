using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class WorkingRangeModel : BaseModel 
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
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
    }
}
