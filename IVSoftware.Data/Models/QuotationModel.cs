using IVSoftware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Data.Models
{
    public class QuotationModel : BaseModelData
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [DisplayName("Nombre del cliente")]
        public string ClientName { get; set; }
        [DisplayName("Dirección del cliente")]
        public string ClientAddress { get; set; }
        [DisplayName("Número de teléfono del cliente")]
        public string ClientPhoneNumber { get; set; }
        [DisplayName("Nombre de contacto")]
        public string ClientContactName { get; set; }
        [DisplayName("Cargo de la persona de contacto")]
        public string ClientContactPosition { get; set; }
        public string Greetings { get; set; }
        public string Exceptions { get; set; }
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Fecha de reserva")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }
        public string ReservationMessage { get; set; }
        public float MeasurementSubtotal { get; set; }
        [DisplayName("Valor total para muestras")]
        public float MeasurementTotalValue { get; set; }
        public float ServicesSubtotal { get; set; }
        [DisplayName("Valor total para servicios")]
        public float ServicesTotalValue { get; set; }
    }
}
