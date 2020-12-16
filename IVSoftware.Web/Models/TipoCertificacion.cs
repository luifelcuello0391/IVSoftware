using System.Collections.Generic;
using System.ComponentModel;

namespace IVSoftware.Web.Models
{
    public class TipoCertificacion
    {
        public int Id { get; set; }

        [DisplayName("Tipo de certificación")]
        public string Nombre { get; set; }

        public virtual List<Formacion> ListFormacion { get; set; }
    }
}
