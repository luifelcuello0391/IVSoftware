using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class TipoInduccion
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }
    }
}
