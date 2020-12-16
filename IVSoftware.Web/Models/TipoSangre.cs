using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class TipoSangre
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
