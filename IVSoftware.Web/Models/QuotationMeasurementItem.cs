using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Models
{
    public class QuotationMeasurementItem : BaseModel
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public int ParameterId { get; set; }
        [DisplayName("Parámetro")]
        public string ParameterName { get; set; }
        public int AnalisisTechniqueId { get; set; }
        [DisplayName("Técnica de análisis")]
        public string AnalisisTechniqueName { get; set; }
        public int ReferenceMethodId { get; set; }
        [DisplayName("Método de referencia")]
        public string ReferenceMethodName { get; set; }
        [DisplayName("Límite de cuantificación")]
        public float QuantificationLimit { get; set; }
        public int MeasurementUnitId { get; set; }
        [DisplayName("Unidad de medida")]
        public string MeasurementUnitName { get; set; } 
        [DisplayName("Valor unitario")]
        public float UnitValue { get; set; }
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
        [DisplayName("Valor total")]
        public float TotalValue { get; set; }
    }
}
