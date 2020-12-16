using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Models
{
    public class ServiceGroupModel : BaseModel
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        public int Code { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        public virtual ICollection<ServiceGroupServicesRelation> Services { get; set; }

        [DisplayName("Cantidad de servicios")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int ServicesCount { get { return Services != null ? Services.Count : 0; } }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [DisplayName("Tiempo de entrega de informe (días)")]
        public int ReportDeliveryTime { 
            get {
                int result = 0;

                if(Services != null && Services.Count > 0)
                {
                    foreach(ServiceGroupServicesRelation service in Services)
                    {
                        if(service.Service != null)
                        {
                            if(result < service.Service.ReportDeliveryTime)
                            {
                                result = service.Service.ReportDeliveryTime;
                            }
                        }
                    }
                }

                return result;
            } }
    }
}
