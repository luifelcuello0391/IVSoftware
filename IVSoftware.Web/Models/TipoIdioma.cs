using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class TipoIdioma
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}