using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class Equipment : BaseModelData
    {
        public Equipment()
        {
            //this.CheckLists = new List<EquipmentCheckList>();
        }

        public int Id { get; set; }
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Marca")]
        public virtual Brand EquipBrand { get; set; }
        [DisplayName("Modelo")]
        public string Model { get; set; }
        [DisplayName("Serie")]
        public string Serie { get; set; }
        [DisplayName("Proveedor")]
        public virtual Provider EquipProvider { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha de compra")]
        public DateTime? PurchaseDate { get; set; }
        [DisplayName("Magnitud")]
        public string Magnitude { get; set; }
        [DisplayName("Rango")]
        public string Range { get; set; }
        [DisplayName("Lectura mínima (resolución)")]
        public float MinimumRead { get; set; }
        [DisplayName("Exactitud")]
        public float Accuracy { get; set; }
        [DisplayName("Alimentación (Voltios)")]
        public float PowerSupply { get; set; }
        [DisplayName("Otra información")]
        public string Observation { get; set; }
        [DisplayName("Recomendaciones especiales")]
        public string OtherRecomendation { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string BrandName { get { return EquipBrand != null && EquipBrand.Name != null && !string.IsNullOrEmpty(EquipBrand.Name.Replace(" ", string.Empty)) ? EquipBrand.Name : "No especifidada"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ProviderName { get { return EquipProvider != null && EquipProvider.Name != null && !string.IsNullOrEmpty(EquipProvider.Name.Replace(" ", string.Empty)) ? EquipProvider.Name : "No especificado"; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int SelectedBrandId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int RequestedProviderId { get; set; }
        public virtual List<MaintenanceModel> MaintenancesForEquipment { get; set; }
    }
}
