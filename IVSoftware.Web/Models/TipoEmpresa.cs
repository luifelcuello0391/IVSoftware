using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class TipoEmpresa
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tipo de empresa")]
        public string Nombre { get; set; }

        public virtual List<ExperienciaLaboral> ListExperienciaLaboral { get; set; }
    }
}
