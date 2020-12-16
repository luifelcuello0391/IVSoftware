using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
}
